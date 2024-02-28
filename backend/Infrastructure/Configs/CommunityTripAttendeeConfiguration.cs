using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class CommunityTripAttendeeConfiguration : IEntityTypeConfiguration<CommunityTripAttendee>
    {
        public void Configure(EntityTypeBuilder<CommunityTripAttendee> builder)
        {
            builder.HasKey(k => new { k.CommunityTripId, k.ApplicationUserId });

            builder
                .HasOne(ta => ta.ApplicationUser)
                .WithMany(a => a.AttendeedCommunityTrip)
                .HasForeignKey(ta => ta.ApplicationUserId);

            builder
                .HasOne(ta => ta.CommunityTrip)
                .WithMany(t => t.Attendees)
                .HasForeignKey(ta => ta.CommunityTripId);

            builder
                .ToTable("CommunityTripAttendees");
        }
    }
}