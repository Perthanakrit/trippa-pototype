using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSet List

        public DbSet<Trip> Trips { get; set; }
        public DbSet<TypeOfTrip> TypeOfTrips { get; set; }
        public DbSet<TripAgenda> TripAgendas { get; set; }
        public DbSet<CustomTrip> CustomTrips { get; set; }
        public DbSet<TripAttendee> TripAttendees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TripPhoto> TripPhotos { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplictionUserConfiguration());
            builder.ApplyConfiguration(new TripConfiguration());
            builder.ApplyConfiguration(new CustomTripConfiguration());
            builder.ApplyConfiguration(new TripAttendeeConfiguration());
            builder.ApplyConfiguration(new TripAgendaConfiguration());
            builder.ApplyConfiguration(new TypeOfTripConfiguration());
            builder.ApplyConfiguration(new TripPhotoConfiguration());
        }
    }
}