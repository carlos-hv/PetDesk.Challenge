using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace PetDesk.Challenge.Services.Caching.Providers
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
            return await _cacheClient.GetDbFromConfiguration().GetAsync<T>(key);
        }

        public async Task AddAsync<T>(string key, T value) where T : class
        {
            await _cacheClient.GetDbFromConfiguration().AddAsync(key, value);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _cacheClient.GetDbFromConfiguration().ExistsAsync(key);
        }
    }
}