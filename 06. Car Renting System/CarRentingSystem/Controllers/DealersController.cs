namespace CarRentingSystem.Controllers
{
    using System.Threading.Tasks;

    using CarRentingSystem.Infrastructure;
    using CarRentingSystem.Models.Dealers;
    using CarRentingSystem.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealersController : Controller
    {
        private readonly IDealerService dealerService;

        public DealersController(IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        [Authorize]
        public IActionResult Create() => this.View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BecomeDealerFormModel dealer)
        {
            var userId = this.User.GetId();

            if (await this.dealerService.UserIsDealer(userId))
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(dealer);
            }

            await this.dealerService.Add(dealer.Name!, dealer.PhoneNumber!, userId);

            return this.RedirectToAction(nameof(CarsController.All), "Cars");
        }
    }
}
