﻿using System;
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
    public class MessageController : DefaultApiCRUDController<MessageAddVM, MessageUpdateVM, MessageVM, IMessageService>
    {
        public MessageController(IMessageService service)
             : base(service)
        {

        }

        public virtual JsonResult GetByGroupId(Guid groupId)
        {
            try
            {
                var result = _service.GetMessagesByGroupId(groupId);

                if (result == null)
                    return new JsonResult(APIResult.CreateVM(false, null, AppStatusCode.WRG01001));

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(APIResult.CreateVMWithError(ex, APIResult.CreateVM(false, null, AppStatusCode.ERR01001)));
            }
        }
    }
}