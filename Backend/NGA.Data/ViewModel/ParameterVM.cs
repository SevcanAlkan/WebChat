using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class ParameterVM : BaseVM 
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupCode { get; set; }
        public string Value { get; set; }
        public int OrderIndex { get; set; }
    }

    public class ParameterAddVM : AddVM
    {
    }

    public class ParameterUpdateVM : UpdateVM
    {
    }
}
