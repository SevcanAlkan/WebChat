using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGA.Core;
using NGA.Core.Enum;
using NGA.Core.Model;
using NGA.Core.Parameter;
using NGA.Core.Validation;
using NGA.Data;
using NGA.Data.Logger;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.API.Filter
{
    public class LoggerFilter : IActionFilter
    {
        private readonly NGADbContext con;

        public LoggerFilter(NGADbContext _con)
        {
            con = _con;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (ParameterValue.SYS01001)
                {
                    string requestBody = "";

                    if (filterContext.ActionArguments != null || filterContext.ActionArguments.Count > 0)
                        requestBody = JsonConvert.SerializeObject(filterContext.ActionArguments.ToList());

                    filterContext.HttpContext.Response.Headers["RequestID"] = LogContext.CreateRequestRecord(
                        ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName,
                        ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName,
                        filterContext.HttpContext.Request.Method,
                        filterContext.HttpContext.Request.Host + filterContext.HttpContext.Request.Path,
                        requestBody,
                        ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType.FullName);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Message:" + Ex.Message + "/n Source: " + Ex.Source + "/n StackTrace: " + Ex.StackTrace);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                if (ParameterValue.SYS01001)
                {
                    Guid id = new Guid(filterContext.HttpContext.Response.Headers["RequestID"].FirstOrDefault());

                    if (id != null && id != Guid.Empty)
                    {
                        LogContext.UpdateRequest(id);

                        var result = filterContext.Result;
                        if (result is JsonResult json)
                        {
                            var data = json.Value;
                            if (data != null && data is APIResultVM)
                            {
                                if ((data as APIResultVM).Errors != null && (data as APIResultVM).Errors.Count > 0)
                                {
                                    LogContext.AddErrorRange(id.ToString(), (data as APIResultVM).Errors);
                                    filterContext.HttpContext.Response.StatusCode = 500;
                                }
                            }
                        }

                        LogContext.Save();
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Message:" + Ex.Message + "/n Source: " + Ex.Source + "/n StackTrace: " + Ex.StackTrace);
            }
        }
    }
}
