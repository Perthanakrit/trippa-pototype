using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class CustomTripRepository : BaseRepository<CustomTrip>, ICustomTripRepository
    {
        public CustomTripRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Tuple<Trip, CustomTrip>> GetTripByCustomTripId(Guid id)
        {
            CustomTrip customTrip = await base._context.CustomTrips.FirstOrDefaultAsync(c => c.Id == id);
            Guid tripId = customTrip.TripId;
            Trip trip = await base._context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
            customTrip.Trip = await base._context.Trips.FirstOrDefaultAsync(t => t.Id == tripId); ;


            return new Tuple<Trip, CustomTrip>(trip, customTrip);
        }
    }
}