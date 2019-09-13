using AutoMapper;
using AutoMapper.QueryableExtensions;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<MessageVM> GetMessagesByGroupId(Guid groupId)
        {
            var result = this.Repository.Query().Where(a => a.GroupId == groupId).Select(a => new MessageVM()
            {
                Id = a.Id,
                Text = a.Text,
                GroupId = a.GroupId,
                Date = a.CreateDT,
                UserId = a.UserId
            }).ToList();

            return result;
        }
        #endregion
    }

    public interface IMessageService : IBaseService<MessageAddVM, MessageUpdateVM, MessageVM, Message>
    {
        List<MessageVM> GetMessagesByGroupId(Guid groupId);
    }
}
