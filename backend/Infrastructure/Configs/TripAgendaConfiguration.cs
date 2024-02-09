using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class TripAgendaConfiguration : IEntityTypeConfiguration<TripAgenda>
    {
        public void Configure(EntityTypeBuilder<TripAgenda> builder)
        {
            builder.HasKey(k => new { k.TripId, k.Id });

            builder
                .ToTable("TripAgendas");
        }
    }
}