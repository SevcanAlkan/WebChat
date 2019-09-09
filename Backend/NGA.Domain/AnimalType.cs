using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class AnimalTypeBase : Table
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class AnimalType : AnimalTypeBase
    {
        //Foreign keys
        public virtual ICollection<Animal> Animals { get; set; }

        public AnimalType()
        {
            Animals = new HashSet<Animal>();
        }       
    }
}
