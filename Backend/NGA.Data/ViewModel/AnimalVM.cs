using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class AnimalVM : BaseVM 
    {
        public string NickName { get; set; }
        public AnimalStatus Status { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public Gender Gender { get; set; }
    }

    public class AnimalAddVM : AddVM
    {
        [Required, MaxLength(100)]
        public string NickName { get; set; }
        [Required, Range(1, 5), DefaultValue(1)]
        public AnimalStatus Status { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public Guid TypeId { get; set; }
        [Required, Range(1, 3), DefaultValue(1)]
        public Gender Gender { get; set; }
    }

    public class AnimalUpdateVM : UpdateVM
    {
        [Required, MaxLength(100)]
        public string NickName { get; set; }
        [Required, Range(1, 5), DefaultValue(1)]
        public AnimalStatus Status { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public Guid TypeId { get; set; }
        [Required, Range(1, 3), DefaultValue(1)]
        public Gender Gender { get; set; }
    }
}
