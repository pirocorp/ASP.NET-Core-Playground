namespace CarRentingSystem.Controllers
{
    using System.Threading.Tasks;

    using CarRentingSystem.Infrastructure.Extensions;
    using CarRentingSystem.Models.Dealers;
    using CarRentingSystem.Services.Dealers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Authorize]
    public class DealersController : Controller
    {
        private readonly IDealerService dealerService;

        public DealersController(IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        public IActionResult Create() => this.View();

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

            await this.dealerService.CreateDealer(dealer.Name!, dealer.PhoneNumber!, userId);

            this.TempData[GlobalMessageKey] = "Successfully become dealer.";

            return this.RedirectToAction(nameof(CarsController.All), "Cars");
        }
    }
}
