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
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.Fee).IsRequired();
            builder.Property(e => e.Origin).IsRequired();
            builder.Property(e => e.Destination).IsRequired();
            builder.Property(e => e.MaxAttendees).IsRequired();
            builder.Property(e => e.TypeOfTripId).IsRequired();
            builder.Property(e => e.HostId).IsRequired();

            // TripAgenda
            builder
                .HasMany(a => a.TripAgenda)
                .WithOne(a => a.Trip)
                .HasForeignKey(a => a.TripId)
                .IsRequired();


            // TripAttendee : 
            builder
                .HasMany(a => a.Attendee)
                .WithOne(a => a.Trip)
                .HasForeignKey(a => a.TripId);

            // Photo : One to Many
            builder
                .HasMany(p => p.Photos)
                .WithOne(p => p.Trip)
                .HasForeignKey(p => p.TripId);

            builder
                .ToTable("Trips");
        }
    }
}