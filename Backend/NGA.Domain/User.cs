using NGA.Core.EntityFramework;
using NGA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class UserBase : Table
    {
        public string UserName { get; set; }
        public string PaswordHash { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string About { get; set; }

        public UserStatus Status { get; set; }
    }

    public class User : UserBase
    {
        //Foreign keys
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<GroupUser> Groups { get; set; }

        public User()
        {
            Messages = new HashSet<Message>();
            Groups = new HashSet<GroupUser>();
        }
    }
}
