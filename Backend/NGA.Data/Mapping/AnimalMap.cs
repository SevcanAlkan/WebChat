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
    public class AnimalMap : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.ToTable("Animal");

            builder.Property(c => c.NickName).HasMaxLength(100);
            builder.Property(c => c.Status).HasDefaultValue(AnimalStatus.NoInfo).IsRequired();
            builder.Property(c => c.Gender).HasDefaultValue(Gender.NoInfo).IsRequired();
            //builder.Property(c => c.TypeId).("GuidValidationAttribute", GuidValidationAttribute);
        }
    }
}
