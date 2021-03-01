using System;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace PetDesk.Challenge.Caching.Providers
{
    public class Redis
    {
        private readonly IRedisCacheClient _cacheClient;

        public Redis(IRedisCacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            return await _cacheClient.Db0.GetAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            await _cacheClient.Db0.AddAsync(key, value, DateTimeOffset.MaxValue);
        }
    }
}