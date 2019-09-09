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

        public UserRole Role { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }

    public class User : UserBase
    {
        //Foreign keys

        public User()
        {
        }
    }
}
