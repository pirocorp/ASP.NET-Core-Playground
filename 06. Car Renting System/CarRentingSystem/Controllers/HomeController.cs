﻿namespace CarRentingSystem.Controllers
{
    using System.Threading.Tasks;

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
                Cars = await this.carService.GetLatestCars(WebConstants.CarouselSize),
            };

            return this.View(viewModel);
        }

        public IActionResult Error() => this.View();
    }
}
