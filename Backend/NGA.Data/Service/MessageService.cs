using AutoMapper;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Service
{
    public class MessageService : BaseService<MessageAddVM, MessageUpdateVM, MessageVM, Message>, IMessageService
    {
        #region Ctor

        public MessageService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface IMessageService : IBaseService<MessageAddVM, MessageUpdateVM, MessageVM, Message>
    {

    }
}
