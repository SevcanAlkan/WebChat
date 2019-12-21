using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Core.Validation;
using NGA.Data.SubStructure;

namespace NGA.MonolithAPI.Controllers.V2
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class DefaultApiController : ControllerBase
    {
        public DefaultApiController()
        {
        }
    }
}