using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key

            builder.Property(a => a.Email)
                .HasMaxLength(50);


            builder.Property(a => a.Phone)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(a => a.Facebook)
                .HasMaxLength(50);


            builder.Property(a => a.Line)
                .HasMaxLength(20);

            builder
                .ToTable("Contacts");
        }
    }
}