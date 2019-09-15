using NGA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Domain
{
    public class GroupUserBase : Table
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUser : GroupUserBase
    {
        //Foreign keys
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }

        public GroupUser()
        {
        }       
    }
}
