using EgeBilgiTaskCase.Application.Abstractions.Caching;
using EgeBilgiTaskCase.Application.Attributes;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EgeBilgiTaskCase.Infrastructure.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
            _connectionMultiplexer = connectionMultiplexer;

        }
        public bool IsConnected => _connectionMultiplexer.IsConnected;

        public async Task DeleteAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
                return default;
            //1 saat ekle erişim olduğunda
            // await _database.KeyExpireAsync(key, TimeSpan.FromHours(1), CommandFlags.FireAndForget);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, serializedValue, expiration ?? TimeSpan.FromHours(3));
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync<T>(string cachePrefixName, int pageIndex, Func<int, Task<PaginatedList<T>>> data) where T : class
        {
            int minPageIndex = pageIndex;
            int maxPageIndex = pageIndex + 2;

            if (pageIndex >= 1)
            {
                int prevMaxPageIndex = minPageIndex - 1;
                int prevMinPageIndex = minPageIndex - 2;

                for (int i = prevMinPageIndex; i <= prevMaxPageIndex; i++)
                {
                    if (i >= 1)
                    {
                        string oldPageKey = $"{cachePrefixName}_{i}";
                        await DeleteAsync(oldPageKey);
                    }
                }
            }
            for (int i = minPageIndex; i <= maxPageIndex; i++)
            {
                string pageKey = $"{cachePrefixName}_{i}";
                var cachedData = await GetAsync<PaginatedList<T>>(pageKey);
                if (cachedData == null)
                {
                    var pageData = await data(i);
                    if (pageData.Data.Count > 0)
                    {
                        await SetAsync(pageKey, pageData, TimeSpan.FromHours(3));
                    }
                }
            }
            string currentPageKey = $"{cachePrefixName}_{pageIndex}";
            return await GetAsync<PaginatedList<T>>(currentPageKey);
        }

        public async Task<List<T>> GetDataListAsync<T>(string cacheName)
        {
            var cachedData = await GetAsync<List<T>>(cacheName);
            if (cachedData != null)
                return cachedData;

            return null;
        }
    }
}
