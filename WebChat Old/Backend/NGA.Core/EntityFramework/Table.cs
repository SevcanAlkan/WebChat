using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.EntityFramework
{
    public interface ITable : IBase
    {
        DateTime CreateDT { get; set; }
        DateTime? UpdateDT { get; set; }
        Guid CreateBy { get; set; }
        Guid? UpdateBy { get; set; }
    }
    public class Table : Base, ITable
    {
        public DateTime CreateDT { get; set; }
        public DateTime? UpdateDT { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
    }
}
