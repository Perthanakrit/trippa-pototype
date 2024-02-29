using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class CommunityTripPhotoConfiguration : IEntityTypeConfiguration<CommunityTripPhoto>
    {
        public void Configure(EntityTypeBuilder<CommunityTripPhoto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Url).IsRequired();

            builder.HasOne(a => a.CommunityTrip)
                .WithMany(a => a.Photos)
                .HasForeignKey(a => a.CommunityTripId);

            builder
                .ToTable("CommunityTripPhotos");
        }
    }
}