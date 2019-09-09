using NGA.Core.Enum;
using NGA.Core.Model;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class LogVM : BaseVM
    {
        public DateTime CreateDate { get; set; }
        public int ResponseTime { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ReturnTypeName { get; set; }
        public string Path { get; set; }
        public HTTPMethodType MethodType { get; set; }
        public string RequestBody { get; set; }
        public List<LogError> Errors { get; set; }
    }
}
