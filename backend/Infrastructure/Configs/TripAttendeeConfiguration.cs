using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class TripAttendeeConfiguration : IEntityTypeConfiguration<TripAttendee>
    {
        public void Configure(EntityTypeBuilder<TripAttendee> builder)
        {
            builder.HasKey(a => new { a.ApplicationUserId, a.TripId }); // Primary Key

            builder
                .HasOne(ta => ta.ApplicationUser)
                .WithMany(a => a.AttendeedTrips)
                .HasForeignKey(ta => ta.ApplicationUserId);

            builder
                .HasOne(ta => ta.Trip)
                .WithMany(t => t.Attendee)
                .HasForeignKey(ta => ta.TripId);

            builder
                .ToTable("TripAttendees");
        }

    }
}