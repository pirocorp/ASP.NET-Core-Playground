namespace CarRentingSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Cars;
    using CarRentingSystem.Services.Models.Cars;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static CarRentingSystem.WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly ICarService carService;
        private readonly IMemoryCache memoryCache;

        public HomeController(
            ICarService carService,
            IMemoryCache memoryCache)
        {
            this.carService = carService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            var latestCars = this.memoryCache
                .Get<IEnumerable<CarLatestServiceModel>>(LatestCarsCacheKey);

            if (latestCars is not null)
            {
                return this.View(latestCars);
            }

            latestCars = await this.carService.GetLatestCars(WebConstants.CarouselSize);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            this.memoryCache
                .Set(LatestCarsCacheKey, latestCars, cacheOptions);

            return this.View(latestCars);
        }

        public IActionResult Error() => this.View();
    }
}
