namespace CarRentingSystem.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CarRentingSystem.Infrastructure;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;
        private readonly IDealerService dealerService;

        public CarsController(
            ICarService carService,
            ICategoryService categoryService,
            IDealerService dealerService)
        {
            this.carService = carService;
            this.categoryService = categoryService;
            this.dealerService = dealerService;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            if (!await this.dealerService.UserIsDealer(this.User.GetId()))
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return this.View(new AddCarFormModel
            {
                Categories = await this.categoryService.GetCategories(),
                Year = DateTime.UtcNow.Year,
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormModel car)
        {
            var dealerId = await this.dealerService.GetDealerId(this.User.GetId());

            if (dealerId is 0)
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!await this.categoryService.Exists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Invalid Category");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = await this.categoryService.GetCategories();
                return this.View(car);
            }

            await this.carService.Add(
                car.Brand!,
                car.Model!,
                car.Description!,
                car.ImageUrl!,
                car.Year,
                car.CategoryId,
                dealerId);

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
