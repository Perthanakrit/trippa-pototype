using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class TypeOfTripConfiguration : IEntityTypeConfiguration<TypeOfTrip>
    {
        public void Configure(EntityTypeBuilder<TypeOfTrip> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(125);

            builder
                .ToTable("TypeOfTrips");
        }
    }
}