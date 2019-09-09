using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Model
{
    public interface IIsResultVM
    {
        bool Result { get; set; }
    }
    
    public class APIResultVM : IIsResultVM
    {
        public Guid? RecId { get; set; }
     
        public object Rec { get; set; }
        public bool Result { get; set; }
        public string StatusCode { get; set; }
        [JsonIgnore]
        public List<APIErrorVM> Errors { get; set; }
    }

    public class APIErrorVM 
    {
        public Guid ErrorId { get; set; }
        public Guid? RequestId { get; set; }

        public DateTime DateTime { get; set; }

        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }

        public string InnerException { get; set; }
    }
}
