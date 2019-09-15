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
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");

            builder.Property(c => c.Text).IsRequired().HasMaxLength(500);
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.GroupId).IsRequired();
        }
    }
}
