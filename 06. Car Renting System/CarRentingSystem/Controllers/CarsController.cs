namespace CarRentingSystem.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarRentingSystem.Infrastructure;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Services.Cars;
    using CarRentingSystem.Services.Dealers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Authorize]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public CarsController(
            ICarService carService,
            IDealerService dealerService,
            IMapper mapper)
        {
            this.carService = carService;
            this.dealerService = dealerService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Add()
        {
            if (!await this.dealerService.UserIsDealer(this.User.GetId()))
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return this.View(new CarFormModel
            {
                Categories = await this.carService.GetCategories(),
                Year = DateTime.UtcNow.Year,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel car)
        {
            var dealerId = await this.dealerService.GetDealerId(this.User.GetId());

            if (dealerId is 0)
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!await this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Invalid Category");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = await this.carService.GetCategories();

                return this.View(car);
            }

            await this.carService.CreateCar(
                car.Brand!,
                car.Model!,
                car.Description!,
                car.ImageUrl!,
                car.Year,
                car.CategoryId,
                dealerId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();

            if (!await this.dealerService.UserIsDealer(userId) && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var car = await this.carService.GetCarDetails(id);

            if (car is null)
            {
                return this.BadRequest();
            }

            if (car.UserId != userId && !this.User.IsAdmin())
            {
                return this.Unauthorized();
            }

            var carForm = this.mapper.Map<CarFormModel>(car);
            carForm.Categories = await this.carService.GetCategories();

            return this.View(carForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel car)
        {
            var dealerId = await this.dealerService.GetDealerId(this.User.GetId());

            if (dealerId is 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!await this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Invalid Category");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = await this.carService.GetCategories();

                return this.View(car);
            }

            if (!await this.carService.CarIsOwnedByDealer(id, dealerId) && !this.User.IsAdmin())
            {
                return this.BadRequest();
            }

            await this.carService.EditCar(
                id,
                car.Brand!,
                car.Model!,
                car.Description!,
                car.ImageUrl!,
                car.Year,
                car.CategoryId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> MyCars()
        {
            var userCars = await this.carService.GetUserCars(this.User.GetId());

            return this.View(userCars);
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllCarsQueryModel query)
        {
            var carsQueryResult = await this.carService
                .GetCars(
                    query.Brand,
                    query.SearchTerm,
                    query.Sorting,
                    query.CurrentPage,
                    CarsPageSize);

            query.Brands = await this.carService.GetBrands();
            query.Cars = carsQueryResult.Cars;
            query.TotalCars = carsQueryResult.TotalCars;

            return this.View(query);
        }
    }
}
