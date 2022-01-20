namespace CarRentingSystem.Controllers.Api
{
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Statistics;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
            => this.Ok(await this.statisticsService.Total());
    }
}
