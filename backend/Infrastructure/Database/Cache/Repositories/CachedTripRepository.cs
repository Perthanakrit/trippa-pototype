using System.Linq.Expressions;
using System.Text.Json.Serialization;
using AutoMapper.Execution;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Database.Cache.Repositories
{
    public class CachedTripRepository : ITripRepository
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDistributedCache _distributedCache;

        public CachedTripRepository(ITripRepository tripRepository, IDistributedCache distributedCache)
        {
            _tripRepository = tripRepository;
            _distributedCache = distributedCache;
        }

        public Task<Trip> AddAsync(Trip entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(Expression<Func<Trip, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Trip>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> GetById(Guid id)
        {
            string key = $"Trip-{id}";
            string cachedTrip = await _distributedCache.GetStringAsync(key);

            Trip trip;
            if (string.IsNullOrEmpty(cachedTrip))
            {
                trip = await _tripRepository.GetById(id);

                if (trip is null)
                {
                    return trip;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(trip));

                return trip;
            }

            trip = JsonConvert.DeserializeObject<Trip>(cachedTrip,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return trip;
        }

        public Task<Trip> GetByIdCache(Guid id)
        {
            throw new NotImplementedException();
        }

        public Trip Remove(Trip entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Trip Update(Trip entity)
        {
            throw new NotImplementedException();
        }
    }
}