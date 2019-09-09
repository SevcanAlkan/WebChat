using AutoMapper;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.API.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region Animal
            CreateMap<Animal, AnimalVM>();
            CreateMap<Animal, AnimalAddVM>();
            CreateMap<Animal, AnimalUpdateVM>();

            CreateMap<AnimalVM, Animal>();
            CreateMap<AnimalVM, AnimalAddVM>();
            CreateMap<AnimalVM, AnimalUpdateVM>();

            CreateMap<AnimalAddVM, Animal>();
            CreateMap<AnimalAddVM, AnimalVM>();
            CreateMap<AnimalAddVM, AnimalUpdateVM>();

            CreateMap<AnimalUpdateVM, Animal>();
            CreateMap<AnimalUpdateVM, AnimalVM>();
            CreateMap<AnimalUpdateVM, AnimalAddVM>();
            #endregion

            #region AnimalType
            CreateMap<AnimalType, AnimalTypeVM>();
            CreateMap<AnimalType, AnimalTypeAddVM>();
            CreateMap<AnimalType, AnimalTypeUpdateVM>();

            CreateMap<AnimalTypeVM, AnimalType>();
            CreateMap<AnimalTypeVM, AnimalTypeAddVM>();
            CreateMap<AnimalTypeVM, AnimalTypeUpdateVM>();

            CreateMap<AnimalTypeAddVM, AnimalType>();
            CreateMap<AnimalTypeAddVM, AnimalTypeVM>();
            CreateMap<AnimalTypeAddVM, AnimalTypeUpdateVM>();

            CreateMap<AnimalTypeUpdateVM, AnimalType>();
            CreateMap<AnimalTypeUpdateVM, AnimalTypeVM>();
            CreateMap<AnimalTypeUpdateVM, AnimalTypeAddVM>();
            #endregion
            
            #region Parameter
            CreateMap<Parameter, ParameterVM>();
            CreateMap<Parameter, ParameterAddVM>();
            CreateMap<Parameter, ParameterUpdateVM>();

            CreateMap<ParameterVM, Parameter>();
            CreateMap<ParameterVM, ParameterAddVM>();
            CreateMap<ParameterVM, ParameterUpdateVM>();

            CreateMap<ParameterAddVM, Parameter>();
            CreateMap<ParameterAddVM, ParameterVM>();
            CreateMap<ParameterAddVM, ParameterUpdateVM>();

            CreateMap<ParameterUpdateVM, Parameter>();
            CreateMap<ParameterUpdateVM, ParameterVM>();
            CreateMap<ParameterUpdateVM, ParameterAddVM>();
            #endregion
            
            #region Nest
            CreateMap<Nest, NestVM>();
            CreateMap<Nest, NestAddVM>();
            CreateMap<Nest, NestUpdateVM>();

            CreateMap<NestVM, Nest>();
            CreateMap<NestVM, NestAddVM>();
            CreateMap<NestVM, NestUpdateVM>();

            CreateMap<NestAddVM, Nest>();
            CreateMap<NestAddVM, NestVM>();
            CreateMap<NestAddVM, NestUpdateVM>();

            CreateMap<NestUpdateVM, Nest>();
            CreateMap<NestUpdateVM, NestVM>();
            CreateMap<NestUpdateVM, NestAddVM>();
            #endregion
            
            #region NestAnimal
            CreateMap<NestAnimal, NestAnimalVM>();
            CreateMap<NestAnimal, NestAnimalAddVM>();
            CreateMap<NestAnimal, NestAnimalUpdateVM>();

            CreateMap<NestAnimalVM, NestAnimal>();
            CreateMap<NestAnimalVM, NestAnimalAddVM>();
            CreateMap<NestAnimalVM, NestAnimalUpdateVM>();

            CreateMap<NestAnimalAddVM, NestAnimal>();
            CreateMap<NestAnimalAddVM, NestAnimalVM>();
            CreateMap<NestAnimalAddVM, NestAnimalUpdateVM>();

            CreateMap<NestAnimalUpdateVM, NestAnimal>();
            CreateMap<NestAnimalUpdateVM, NestAnimalVM>();
            CreateMap<NestAnimalUpdateVM, NestAnimalAddVM>();
            #endregion

            #region User
            CreateMap<User, UserVM>();
            CreateMap<User, UserAddVM>();
            CreateMap<User, UserUpdateVM>();

            CreateMap<UserVM, User>();
            CreateMap<UserVM, UserAddVM>();
            CreateMap<UserVM, UserUpdateVM>();

            CreateMap<UserAddVM, User>();
            CreateMap<UserAddVM, UserVM>();
            CreateMap<UserAddVM, UserUpdateVM>();

            CreateMap<UserUpdateVM, User>();
            CreateMap<UserUpdateVM, UserVM>();
            CreateMap<UserUpdateVM, UserAddVM>();
            #endregion

        }
    }

   
}
