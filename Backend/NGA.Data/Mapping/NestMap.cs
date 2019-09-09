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
    public class NestMap : IEntityTypeConfiguration<Nest>
    {
        public void Configure(EntityTypeBuilder<Nest> builder)
        {
            builder.ToTable("Nest");

            builder.Property(c => c.Name).HasMaxLength(100);            
            builder.Property(c=> c.Status).HasDefaultValue(NestStatus.NoInfo).IsRequired();
           
        }
    }
}
