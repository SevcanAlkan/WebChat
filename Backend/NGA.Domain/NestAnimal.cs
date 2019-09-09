using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class NestAnimalBase : Table
    {
        public Guid NestId { get; set; }
        public Guid AnimalId { get; set; }
    }

    public class NestAnimal : NestAnimalBase
    {
        //Foreign keys
        public Nest Nest { get; set; }
        public Animal Animal { get; set; }

        public NestAnimal()
        {

        }
    }
}
