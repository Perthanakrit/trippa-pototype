using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configs
{

    public class TripPhotoConfiguration : IEntityTypeConfiguration<TripPhoto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TripPhoto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Url).IsRequired();
            builder.Property(a => a.TripId).IsRequired();
            builder.HasOne(a => a.Trip)
                .WithMany(a => a.Photos)
                .HasForeignKey(a => a.TripId);

            builder
                .ToTable("TripPhotos");
        }
    }
}