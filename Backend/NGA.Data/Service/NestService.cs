using AutoMapper;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Service
{
    public class NestService : BaseService<NestAddVM, NestUpdateVM, NestVM, Nest>, INestService
    {
        #region Ctor

        public NestService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface INestService : IBaseService<NestAddVM, NestUpdateVM, NestVM, Nest>
    {

    }
}
