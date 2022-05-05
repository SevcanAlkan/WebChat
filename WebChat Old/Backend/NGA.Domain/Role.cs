using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class RoleBase : IdentityRole<Guid>
    {
        public bool CanManageGroups { get; set; }
        public bool CanJoinAnyGroup { get; set; }

    }

    public class Role : RoleBase
    {
        //Foreign keys
       // public virtual ICollection<User> Users { get; set; }

        public Role()
        {
         //   Users = new HashSet<User>();
        }
    }
}
