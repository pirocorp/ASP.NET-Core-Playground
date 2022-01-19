namespace CarRentingSystem.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Services;

    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public CarsController(
            ICarService carService,
            ICategoryService categoryService)
        {
            this.carService = carService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Add() => this.View(new AddCarFormModel
        {
            Categories = await this.categoryService.GetCategories(),
            Year = DateTime.UtcNow.Year,
        });

        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormModel car)
        {
            if (!await this.categoryService.Exists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Invalid Category");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = await this.categoryService.GetCategories();
                return this.View(car);
            }

            await this.carService.Add(car.Brand!, car.Model!, car.Description!, car.ImageUrl!, car.Year, car.CategoryId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All([FromQuery]AllCarsQueryModel query)
        {
            var (totalCars, carsInPage) = await this.carService.GetAll(query.Brand, query.SearchTerm, query.Sorting, query.CurrentPage);

            query.Brands = await this.carService.GetBrands();
            query.Cars = carsInPage;
            query.TotalCars = totalCars;

            return this.View(query);
        }
    }
}
