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

    

        #endregion
    }

    public interface IUserService 
    {
    }
}
