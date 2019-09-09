using AutoMapper;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NGA.Data.Service
{
    public class GroupService : BaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>, IGroupService
    {
        #region Ctor

        public GroupService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        
        #endregion
    }

    public interface IGroupService : IBaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>
    {
    }
}
