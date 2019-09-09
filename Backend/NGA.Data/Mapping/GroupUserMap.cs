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
    public class GroupUserMap : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("GroupUser");

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.GroupId).IsRequired();

        }
    }
}
