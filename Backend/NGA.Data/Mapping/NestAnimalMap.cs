using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGA.Core.Enum;
using NGA.Core.Validation;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.Mapping
{
    public class NestAnimalMap : IEntityTypeConfiguration<NestAnimal>
    {
        public void Configure(EntityTypeBuilder<NestAnimal> builder)
        {
            builder.ToTable("NestAnimal");
            
        }
    }
}
