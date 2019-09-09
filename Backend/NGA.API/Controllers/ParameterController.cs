using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.API.Controllers
{
    public class ParameterController : DefaultApiController<ParameterAddVM, ParameterUpdateVM, ParameterVM, IParameterService>
    {
        public ParameterController(IParameterService service)
             : base(service)
        {

        }
        
        public override Task<JsonResult> Add(ParameterAddVM model)
        {
            return base.Add(null);
        }

        public override Task<JsonResult> Update(Guid id, ParameterUpdateVM model)
        {
            return base.Update(Guid.Empty, null);
        }

        public override Task<JsonResult> Delete(Guid id)
        {
            return base.Delete(Guid.Empty);
        }
    }
}
