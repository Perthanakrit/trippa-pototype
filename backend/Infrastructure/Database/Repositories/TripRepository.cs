using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Database.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        private readonly IDistributedCache _cache;
        public TripRepository(DatabaseContext context, IDistributedCache cache) : base(context)
        {
            _cache = cache;

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

    }
}