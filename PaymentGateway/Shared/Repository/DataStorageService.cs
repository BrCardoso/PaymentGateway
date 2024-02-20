using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Application.Shared.Repository
{
    public class DataStorageService : IDataStorageService
    {
        private readonly IMemoryCache _memoryCache;

        public DataStorageService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<T> Get<T>(Guid id)
        {
            var result = _memoryCache.Get<string>(id);
            return Task.FromResult(JsonConvert.DeserializeObject<T>(result));
        }

        public Task<bool> Set<T>(Guid id, T value)
        {
            _memoryCache.Set(id, JsonConvert.SerializeObject(value));
            return Task.FromResult(true);
        }
    }
}
