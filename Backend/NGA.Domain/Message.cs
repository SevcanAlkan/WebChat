using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class MessageBase : Table
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class Message : MessageBase
    {
        //Foreign keys
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }

        public Message()
        {
        }
    }
}
