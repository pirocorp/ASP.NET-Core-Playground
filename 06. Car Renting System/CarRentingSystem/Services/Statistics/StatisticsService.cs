namespace CarRentingSystem.Services.Statistics
{
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Services.Models.Statistics;

    using Microsoft.EntityFrameworkCore;

    public class StatisticsService : IStatisticsService
    {
        private readonly CarRentingDbContext dbContext;

        public StatisticsService(CarRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<StatisticsServiceModel> Total()
        {
            var statistics = new StatisticsServiceModel()
            {
                TotalCars = await this.dbContext.Cars.CountAsync(),
                TotalRents = 0,
                TotalUsers = await this.dbContext.Users.CountAsync(),
            };

            return statistics;
        }
    }
}
