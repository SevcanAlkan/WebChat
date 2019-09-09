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

        public UserRole Role { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }

    public class UserAddVM : AddVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string PaswordHash { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        [Required, Range(1, 3), DefaultValue(1)]
        public UserRole Role { get; set; }
        [DefaultValue(0)]
        public bool IsApproved { get; set; }
        [DefaultValue(0)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(250)]
        public string Bio { get; set; }
    }

    public class UserUpdateVM : UpdateVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string PaswordHash { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        [Required, Range(1, 3), DefaultValue(1)]
        public UserRole Role { get; set; }
        [DefaultValue(0)]
        public bool IsApproved { get; set; }
        [DefaultValue(0)]
        public bool IsBanned { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(250)]
        public string Bio { get; set; }
    }
}
