using AutoMapper;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Service
{
    public class UserService : BaseService<UserAddVM, UserUpdateVM, UserVM, User>, IUserService
    {
        #region Ctor

        public UserService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface IUserService : IBaseService<UserAddVM, UserUpdateVM, UserVM, User>
    {

    }
}
