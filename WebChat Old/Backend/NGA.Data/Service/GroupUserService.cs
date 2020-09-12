using AutoMapper;
using AutoMapper.QueryableExtensions;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Core.Validation;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGA.Data.Service
{
    public class GroupUserService : BaseService<GroupUserAddVM, GroupUserUpdateVM, GroupUserVM, GroupUser>, IGroupUserService
    {
        #region Ctor

        public GroupUserService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                


        #endregion
    }

    public interface IGroupUserService : IBaseService<GroupUserAddVM, GroupUserUpdateVM, GroupUserVM, GroupUser>
    {
    }
}
