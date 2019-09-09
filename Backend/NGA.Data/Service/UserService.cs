using AutoMapper;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
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
    public class UserService : BaseService<UserAddVM, UserUpdateVM, UserVM, User>, IUserService
    {
        #region Ctor

        public UserService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        public async Task<APIResultVM> Authenticate(string username, string password)
        {
            User user = await Task.Run(() => Repository.Query().FirstOrDefault(x => x.UserName == username && x.PaswordHash == password));

            //Update last login

            if (user == null)
                return APIResult.CreateVM(false, null, AppStatusCode.WRG01003);

            UserVM vm = new UserVM();
            vm = mapper.Map<User, UserVM>(user, vm);
            
            return APIResult.CreateVMWithRec(vm, true, vm.Id);
        }

        #endregion
    }

    public interface IUserService : IBaseService<UserAddVM, UserUpdateVM, UserVM, User>
    {
        Task<APIResultVM> Authenticate(string username, string password);
    }
}
