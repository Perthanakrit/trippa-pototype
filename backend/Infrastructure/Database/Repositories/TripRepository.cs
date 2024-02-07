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
        // Inherit the methods from the base repository

        public async Task<Trip> GetByIdCache(Guid id)
        {
            Trip trip;
            string key = $"Trip-{id}";
            string cachedTrip = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedTrip))
            {
                trip = await base.GetById(id);

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
    }
}