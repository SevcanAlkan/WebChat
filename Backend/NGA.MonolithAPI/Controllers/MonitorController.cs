using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using NGA.Data.ViewModel;

namespace NGA.MonolithAPI.Controllers.V2
{
    //SOURCE: https://stackoverflow.com/questions/41908957/get-all-registered-routes-in-asp-net-core

    [Route("api/monitor")]
    public class MonitorController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public MonitorController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes()
        {
            List<APIRouteVM> routes = _provider.ActionDescriptors.Items.Select(x => new APIRouteVM
            {
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                Parameters = JsonConvert.SerializeObject(x.Parameters.Select(s => new { Name = s.Name, Type = s.ParameterType.Name }).ToList()),
                Path = x.AttributeRouteInfo.Template
            }).ToList();

            return Ok(routes);
        }
    }
}