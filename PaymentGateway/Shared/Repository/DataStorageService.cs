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

        public T Get<T>(Guid id)
        {
            var result = _memoryCache.Get<string>(id);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public string Set(Guid id, string value) =>
            _memoryCache.Set(id, value);
    }
}
