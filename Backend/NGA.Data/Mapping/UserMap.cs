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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(c => c.UserName).IsRequired().HasMaxLength(15);
            builder.Property(c => c.PaswordHash).IsRequired().HasMaxLength(50);

            builder.Property(c=>c.Role).HasDefaultValue(UserRole.Standart).IsRequired();

            builder.Property(c => c.IsApproved).HasDefaultValue(0);
            builder.Property(c => c.IsBanned).HasDefaultValue(0);

            builder.Property(c => c.DisplayName).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Bio).HasMaxLength(250);
        }
    }
}
