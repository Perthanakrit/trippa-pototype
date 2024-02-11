using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class CustomTripRepository : BaseRepository<CustomTrip>, ICustomTripRepository
    {
        private readonly IMapper _mapper;

        public CustomTripRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<CustomTripAndTrip> GetTripByCustomTripId(Guid id)
        {
            var result = await base._context.CustomTrips
                .Where(x => x.Id == id)
                .ProjectTo<CustomTripAndTrip>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<CustomTripAndTrip>> GetTripsInCustomTrips()
        {
            var result = await base._context.CustomTrips
                .ProjectTo<CustomTripAndTrip>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task DeleteIncludeTripAsync(Guid id)
        {
            var trip = await base._context.CustomTrips
                .Include(x => x.Trip).FirstOrDefaultAsync(x => x.Id == id);

            base._context.CustomTrips.Remove(trip);
            base._context.Trips.Remove(trip.Trip);
            await base._context.SaveChangesAsync();
        }
    }
}