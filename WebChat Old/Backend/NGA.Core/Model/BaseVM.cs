using NGA.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Model
{
    public interface IBaseVM
    {
        Guid Id { get; set; }
    }
    public class BaseVM : IBaseVM
    {
        public Guid Id { get; set; }
    }

    public interface ITableVM : IBaseVM
    {
        DateTime CreateDT { get; set; }
        DateTime? UpdateDT { get; set; }
        Guid CreateBy { get; set; }
        Guid UpdateBy { get; set; }
    }
    public class TableVM : BaseVM, ITableVM
    {
        public DateTime CreateDT { get; set; }
        public DateTime? UpdateDT { get; set; }
        public Guid CreateBy { get; set; }
        public Guid UpdateBy { get; set; }
    }

    public interface IAddVM 
    {
    }
    public class AddVM : IAddVM
    {
    }

    public interface IUpdateVM
    {
    }
    public class UpdateVM : IUpdateVM
    {
    }
}
