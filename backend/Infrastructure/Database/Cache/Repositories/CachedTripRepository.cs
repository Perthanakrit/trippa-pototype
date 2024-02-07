using System.Linq.Expressions;
using System.Text.Json.Serialization;
using AutoMapper.Execution;
using Core.Interface.Infrastructure.Database;
using Core.Services;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Database.Cache.Repositories
{
    public class CachedTripRepository
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

        public async Task<List<Trip>> GetAll()
        {
            string key = "Trips";
            string cachedTrip = await _distributedCache.GetStringAsync(key);

            List<Trip> trips;
            if (string.IsNullOrEmpty(cachedTrip))
            {
                trips = await _tripRepository.GetAll();

                if (trips is null)
                {
                    return trips;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(trips));

                return trips;
            }

            trips = JsonConvert.DeserializeObject<List<Trip>>(cachedTrip,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });

            return trips;
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

        public Task<TripServiceResponse> GetTripAsync(Guid provinceId)
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