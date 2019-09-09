using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Domain
{
    public class LogBase : Base
    {
        public DateTime CreateDate { get; set; }
        public int ResponseTime { get; set; }

        [MaxLength(50)]
        public string ControllerName { get; set; }
        [MaxLength(50)]
        public string ActionName { get; set; }
        [MaxLength(250)]
        public string ReturnTypeName { get; set; }

        [MaxLength(250)]
        public string Path { get; set; }

        public HTTPMethodType MethodType { get; set; }

        public string RequestBody { get; set; }
       
    }

    public class Log : LogBase
    {
        //Foreign keys
        public virtual ICollection<LogError> Errors { get; set; }

        public Log()
        {
            Errors = new HashSet<LogError>();
        }
    }
}
