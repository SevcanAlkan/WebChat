using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class UserVM : BaseVM 
    {
        public string UserName { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string About { get; set; }

        public UserStatus Status { get; set; }
    }

    public class UserAddVM : AddVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Pasword { get; set; }

        public DateTime? LastLoginDateTime { get; set; }
      
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(250)]
        public string About { get; set; }

        [Required, Range(1, 4), DefaultValue(1)]
        public UserStatus Status { get; set; }

    }

    public class UserUpdateVM : UpdateVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Pasword { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(250)]
        public string About { get; set; }

        [Required, Range(1, 4), DefaultValue(1)]
        public UserStatus Status { get; set; }
    }
}
