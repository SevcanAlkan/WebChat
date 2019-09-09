using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class AnimalBase : Table
    {
        public string NickName { get; set; }
        public AnimalStatus Status { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid TypeId { get; set; }
        public Gender Gender { get; set; }
    }

    public class Animal : AnimalBase
    {
        //Foreign keys
        public AnimalType Type { get; set; }

        public virtual ICollection<NestAnimal> NestAnimals { get; set; }

        public Animal()
        {
            NestAnimals = new HashSet<NestAnimal>();
        }
    }
}
