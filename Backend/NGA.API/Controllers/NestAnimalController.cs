using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.API.Controllers
{
    public class NestAnimalController : DefaultApiController<NestAnimalAddVM, NestAnimalUpdateVM, NestAnimalVM, INestAnimalService>
    {
        public NestAnimalController(INestAnimalService service)
             : base(service)
        {

        }
    }
}