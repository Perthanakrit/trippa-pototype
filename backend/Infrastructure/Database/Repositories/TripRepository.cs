using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface.Infrastructure.Database;
using Core.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Database.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public TripRepository(DatabaseContext context, IDistributedCache cache, IMapper mapper) : base(context)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public Task AddAttendeeAsync(Trip trip)
        {
            throw new NotImplementedException();
        }

        // Inherit the methods from the base repository

        public async Task<Trip> GetByIdCache(Guid id)
        {
            Trip trip;
            string key = $"Trip-{id}";
            string cachedTrip = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedTrip))
            {
                trip = await base.GetById<Trip>(id);

                if (trip is null)
                {
                    return null;
                }

                await _cache.SetStringAsync(key, JsonConvert.SerializeObject(trip));

                return trip;
            }

            trip = JsonConvert.DeserializeObject<Trip>(cachedTrip,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return trip;
        }

        public async Task<TripServiceResponse> GetTripAsync(Guid provinceId)
        {
            var trip = await base._context.Trips
                            .Where(x => x.Id == provinceId)
                            .ProjectTo<TripServiceResponse>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
            return trip;

        }

        public async Task<List<TripServiceResponse>> GetTripsAsync()
        {
            return await base._context.Trips
                            .ProjectTo<TripServiceResponse>(_mapper.ConfigurationProvider)
                            .ToListAsync();

        }

        public async Task<Trip> ExistedTripAsync(Guid tripId)
        {
            Trip trip = await base._context.Trips
                            .Include(x => x.Attendee).ThenInclude(a => a.ApplicationUser)
                            .FirstOrDefaultAsync(x => x.Id == tripId);

            return trip;
        }

        public async Task<List<TripAttendee>> GetAllAttendeeAsync(Guid tripId)
        {
            List<TripAttendee> attendees = await base._context.TripAttendees
                            .Include(x => x.ApplicationUser)
                            .Where(x => x.TripId == tripId)
                            .ToListAsync();
            return attendees;
        }

        public async Task<Trip> GetOneTrip(Guid tripId)
        {
            return await base._context.Trips
                                .Where(x => x.Id == tripId)
                                .Include(t => t.Attendee).ThenInclude(at => at.ApplicationUser)
                                .Include(t => t.TripAgenda)
                                .Include(t => t.TypeOfTrip)
                                .Include(t => t.Photos)
                                .FirstOrDefaultAsync();
        }
    }
}