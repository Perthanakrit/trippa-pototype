using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class CustomTripConfiguration : IEntityTypeConfiguration<CustomTrip>
    {
        public void Configure(EntityTypeBuilder<CustomTrip> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key
            builder
                .Property(m => m.TripId)
                .IsRequired();
            builder
                .ToTable("CustomTrips");
        }
    }
}