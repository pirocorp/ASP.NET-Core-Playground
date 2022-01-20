namespace CarRentingSystem.Services.Statistics
{
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Models.Statistics;

    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
