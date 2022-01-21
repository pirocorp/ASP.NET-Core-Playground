﻿namespace CarRentingSystem.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Cars;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel()
            {
                Cars = await this.carService.GetLatestCars(3),
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
