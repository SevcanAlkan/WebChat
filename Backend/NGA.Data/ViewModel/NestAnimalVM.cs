using NGA.Core.Enum;
using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGA.Data.ViewModel
{
    public class NestAnimalVM : BaseVM 
    {
        public string NestName { get; set; }
        public Guid NestId { get; set; }

        public string AnimalNickName { get; set; }
        public Guid AnimalId { get; set; }
    }

    public class NestAnimalAddVM : AddVM
    {
        [Required]
        public Guid NestId { get; set; }
        [Required]
        public Guid AnimalId { get; set; }
    }

    public class NestAnimalUpdateVM : UpdateVM
    {
        [Required]
        public Guid NestId { get; set; }
        [Required]
        public Guid AnimalId { get; set; }
    }
}
