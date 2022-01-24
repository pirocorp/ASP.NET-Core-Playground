namespace CarRentingSystem.Controllers.Api
{
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Statistics;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
            => this.Ok(await this.statisticsService.Total());
    }
}
