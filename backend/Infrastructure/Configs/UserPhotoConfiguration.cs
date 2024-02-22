using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configs
{

    public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserPhoto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Url).IsRequired();
            builder.Property(a => a.UserId).IsRequired();

            builder
                .ToTable("UserPhotos");
        }
    }
}