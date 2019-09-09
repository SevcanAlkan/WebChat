using AutoMapper;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Service
{
    public class AnimalService : BaseService<AnimalAddVM, AnimalUpdateVM, AnimalVM, Animal>, IAnimalService
    {
        #region Ctor

        public AnimalService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface IAnimalService : IBaseService<AnimalAddVM, AnimalUpdateVM, AnimalVM, Animal>
    {

    }
}
