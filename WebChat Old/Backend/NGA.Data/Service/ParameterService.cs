using AutoMapper;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Service
{
    public class ParameterService : BaseService<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter>, IParameterService
    {
        #region Ctor

        public ParameterService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface IParameterService : IBaseService<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter>
    {

    }
}
