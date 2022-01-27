namespace CarRentingSystem.Tests.Mocks
{
    using Microsoft.Extensions.Caching.Memory;

    public static class MemoryCacheMock
    {
        public static IMemoryCache Instance
        {
            get
            {
                var memoryCacheOptions = new MemoryCacheOptions();
                var memoryCache = new MemoryCache(memoryCacheOptions);

                return memoryCache;
            }
        }
    }
}
