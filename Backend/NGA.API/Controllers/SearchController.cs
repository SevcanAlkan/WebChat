using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGA.Data.Service;
using NGA.Domain;

namespace NGA.API.Controllers
{
    public class SearchController : DefaultApiController
    {
        private IMessageService _service;

        public SearchController(IMessageService service)
        {
            this._service = service;
        }

        public JsonResult Get(string key)
        {
            if(key == null || key == "" || key.Length < 4)
            {
                return new JsonResult("");
            }

            var messages = _service.Repository.Query().Where(s => s.Text.Contains(key)).ToList();

            return new JsonResult(messages);
        }
    }
}