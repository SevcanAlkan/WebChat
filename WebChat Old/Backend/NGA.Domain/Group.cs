using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class GroupBase : Table
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsOneToOneChat { get; set; }
    }

    public class Group : GroupBase
    {
        //Foreign keys
        public virtual ICollection<GroupUser> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Group()
        {
            Users = new HashSet<GroupUser>();
            Messages = new HashSet<Message>();
        }
    }
}
