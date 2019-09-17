using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class UserService : IUserService
    {
        private NGADbContext con;
        private readonly IMapper mapper;

        #region Ctor

        public UserService(NGADbContext _con, IMapper _mapper)
        {
            con = _con;
            mapper = _mapper;
        }

        #endregion

        #region Methods                

        public List<UserVM> GetAll()
        {
            var userList = con.Set<User>().ToList();
            List<UserVM> result = mapper.Map<List<UserVM>>(userList);

            return result;
        }

        public List<UserListVM> GetUserList()
        {
            var result = con.Set<User>().Select(a => new UserListVM()
            {
                Id = a.Id,
                DisplayName = a.DisplayName,
                IsAdmin = a.IsAdmin,
                UserName = a.UserName
            }).ToList();

            return result;
        }

        public bool Any(string userName)
        {
            var result = con.Set<User>().Any(a => a.UserName == userName);

            return result;
        }

        public User GetById(Guid id)
        {
            var rec = con.Set<User>().Where(a => a.Id == id).FirstOrDefault();

            if (rec == null)
            {
                return null;   
            }

            rec.AccessFailedCount = 0;
            rec.ConcurrencyStamp = "";
            //rec.PasswordHash = "";
            rec.SecurityStamp = "";

            return rec;
        }

        #endregion
    }

    public interface IUserService
    {
        List<UserVM> GetAll();
        List<UserListVM> GetUserList();
        bool Any(string userName);
        User GetById(Guid id);
    }
}
