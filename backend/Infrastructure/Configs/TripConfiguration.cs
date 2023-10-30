using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(125);
            builder
                .Property(m => m.Description)
                .HasMaxLength(255);

            builder.Property(e => e.Landmark);
            builder.Property(e => e.Duration);
            builder.Property(e => e.Price);
            builder.Property(e => e.Fee);
            builder.Property(e => e.Origin);
            builder.Property(e => e.Destination);

            builder
                .ToTable("Trips");
        }
    }
}