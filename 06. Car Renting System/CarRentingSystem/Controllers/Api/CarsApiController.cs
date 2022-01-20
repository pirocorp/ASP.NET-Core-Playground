namespace CarRentingSystem.Controllers.Api
{
    using System.Threading.Tasks;

    using CarRentingSystem.Models.Api.Cars;
    using CarRentingSystem.Services.Cars;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService carService;

        public CarsApiController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AllCarsApiRequestModel query)
            => this.Ok(await this.carService
                .GetCars(query.Brand, query.SearchTerm, query.Sorting, query.Page, query.CarsPerPage));
    }
}
