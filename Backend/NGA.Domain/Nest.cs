using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class NestBase : Table
    {
        public string Name { get; set; }
        public NestStatus Status { get; set; }
        public DateTime? LastRepaireDate { get; set; }
        public DateTime? LastCheckDate { get; set; }

        public double XCordinate { get; set; }
        public double YCordinate { get; set; }
    }

    public class Nest : NestBase
    {
        //Foreign keys
        public virtual ICollection<NestAnimal> NestAnimals { get; set; }

        public Nest()
        {
            NestAnimals = new HashSet<NestAnimal>();
        }
    }
}
