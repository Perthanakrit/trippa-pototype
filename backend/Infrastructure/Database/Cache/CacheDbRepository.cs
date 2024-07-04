using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Database.Cache
{
    public class CacheDbRepository : ICacheDbRespository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheDbRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetData<T>(string key)
        {
            string cachedTrip = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedTrip))
            {
                return default;
            }

            T result = JsonConvert.DeserializeObject<T>(cachedTrip,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return result;
        }

        public async Task RemoveData(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task SetData<T>(string key, T data, TimeSpan? duration = null)
        {
            DistributedCacheEntryOptions options = new()
            {
                AbsoluteExpirationRelativeToNow = duration
            };

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(data), options);
        }
    }
}