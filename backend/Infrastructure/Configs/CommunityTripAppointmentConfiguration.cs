using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class CommunityTripAppointmentConfiguration : IEntityTypeConfiguration<CommunityTripAppointment>
    {
        public void Configure(EntityTypeBuilder<CommunityTripAppointment> builder)
        {
            builder.HasKey(k => new { k.CommuTripId, k.Id });

            builder
                .ToTable("CommunityTripAppointments");
        }
    }
}