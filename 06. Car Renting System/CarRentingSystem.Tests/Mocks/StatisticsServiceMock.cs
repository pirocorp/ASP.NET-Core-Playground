namespace CarRentingSystem.Tests.Mocks
{
    using System.Threading.Tasks;

    using CarRentingSystem.Services.Models.Statistics;
    using CarRentingSystem.Services.Statistics;

    using Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(Task.FromResult(new StatisticsServiceModel()
                    {
                        TotalCars = 5,
                        TotalRents = 10,
                        TotalUsers = 15,
                    }));

                return statisticsServiceMock.Object;
            }
        }
    }
}
