using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class AnimalTypeVM : BaseVM 
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
    }

    public class AnimalTypeAddVM : AddVM
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class AnimalTypeUpdateVM : UpdateVM
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
