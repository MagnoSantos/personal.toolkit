using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Personal.Patterns.Decorator
{
    public class CachingDecorator : IAnyService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAnyService _anyService;

        public CachingDecorator(IMemoryCache memoryCache, IAnyService anyService)
        {
            _memoryCache = memoryCache
                ?? throw new ArgumentNullException(nameof(memoryCache));

            _anyService = anyService
                ?? throw new ArgumentNullException(nameof(anyService));
        }

        public async Task<string> GetAnyValueAsync()
        {
            if (_memoryCache.TryGetValue(CacheKey, out string value))
            {
                return value;
            }

            var valueToCache = await _anyService.GetAnyValueAsync();

            return _memoryCache.Set(CacheKey, valueToCache);
        }

        internal static string CacheKey => "sample_cache_decorator_key";
    }
}