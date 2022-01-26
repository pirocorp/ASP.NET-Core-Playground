namespace CarRentingSystem.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Cars;

    using Microsoft.AspNetCore.Mvc;

    public class CarsController : AdminController
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> All()
        {
            var queryModel = await this.carService.GetCars(publicOnly: false);

            return this.View(queryModel.Cars);
        }

        public async Task<IActionResult> ChangeVisibility(int id)
        {
            await this.carService.ChangeCarVisibility(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
