using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Domain
{
    public class ParameterBase : Table
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupCode { get; set; }
        public string Value { get; set; }
        public int OrderIndex { get; set; }
    }

    public class Parameter : ParameterBase
    {
        #region Foregin Keys
        #endregion
    }
}
