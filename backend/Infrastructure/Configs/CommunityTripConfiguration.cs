using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class CommunityTripConfiguration : IEntityTypeConfiguration<CommunityTrip>
    {
        public void Configure(EntityTypeBuilder<CommunityTrip> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key
            builder
                .Property(m => m.Location)
                .IsRequired()
                .HasMaxLength(125)
                .IsRequired();

            builder
                .Property(m => m.Duration)
                .HasMaxLength(225);

            builder
                .Property(m => m.AgeRange)
                .HasMaxLength(125)
                .IsRequired();

            builder
                .Property(m => m.MaxAttendees)
                .IsRequired();

            builder
                .HasOne(a => a.Appointment)
                .WithOne(a => a.CommuTrip)
                .HasForeignKey<CommunityTripAppointment>(a => a.CommuTripId);

            builder
                .HasMany(a => a.Attendees)
                .WithOne(a => a.CommunityTrip)
                .HasForeignKey(a => a.CommunityTripId);

            builder
                .HasMany(a => a.Photos)
                .WithOne(a => a.CommunityTrip)
                .HasForeignKey(a => a.CommunityTripId);

            builder
                .ToTable("CommunityTrips");
        }
    }
}