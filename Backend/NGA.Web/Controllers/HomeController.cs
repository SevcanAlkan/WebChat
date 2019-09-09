using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NGA.Core;
using NGA.Core.Enum;
using NGA.Data;
using NGA.Data.Logger;
using NGA.Data.ViewModel;
using NGA.Domain;
using NGA.Web.Models;

namespace NGA.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult APILog()
        {
            List<LogVM> logs = new List<LogVM>();

            logs = LogContext.GetLogs();

            return View(logs.OrderByDescending(o=>o.CreateDate).ToList());
        }
        public IActionResult APILogDetail(Guid Id)
        {
            if (Id == null || Id == Guid.Empty)
                return RedirectToAction("APILog");

            LogVM rec = LogContext.GetLog(Id);

            if (rec == null)
                return RedirectToAction("APILog");

            return View(rec);
        }

        public IActionResult APIEndPoint()
        {
            List<APIRouteVM> routes = new List<APIRouteVM>();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSetting["APIHost"]+"/");
                //HTTP GET
                var responseTask = client.GetAsync("monitor/routes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<APIRouteVM>>();
                    readTask.Wait();

                    routes = readTask.Result;
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(routes.OrderBy(o => o.Controller).ThenBy(n => n.Action).ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
