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
    public class AnimalTypeMap : IEntityTypeConfiguration<AnimalType>
    {
        public void Configure(EntityTypeBuilder<AnimalType> builder)
        {
            builder.ToTable("AnimalType");

            builder.Property(c => c.Name).HasMaxLength(100);
        }
    }
}
