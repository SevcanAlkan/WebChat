using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class GroupUserVM : BaseVM 
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUserAddVM : AddVM
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUserUpdateVM : UpdateVM
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
