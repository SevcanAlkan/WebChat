using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        [JsonIgnore]
        public UserStatus Status { get; set; }
        public int StatusVal
        {
            get
            {
                return (int)this.Status;
            }
        }
    }

    public class UserAddVM : AddVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string PasswordHash { get; set; }

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
        public string PasswordHash { get; set; }

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

    public class UserLoginVM
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string PasswordHash { get; set; }
    }

    public class UserAuthenticateVM : BaseVM
    {
        public string UserName { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public string DisplayName { get; set; }
        public string About { get; set; }

        [JsonIgnore]
        public UserStatus Status { get; set; }
        public int StatusVal
        {
            get
            {
                return (int)this.Status;
            }
        }

        public string Token { get; set; }
    }

    public class UserListVM : BaseVM
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string DisplayName { get; set; }
    }
}
