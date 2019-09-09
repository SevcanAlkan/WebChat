using NGA.Core.EntityFramework;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Domain
{
    public class LogErrorBase : Base
    {
        public int OrderNum { get; set; }
        public DateTime DateTime { get; set; }

        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }

        public string InnerException { get; set; }

        public Guid RequestId { get; set; }
    }

    public class LogError : LogErrorBase
    {
        //Foreign keys
        public virtual Log Request { get; set; }

        public LogError()
        {
        }
    }
}
