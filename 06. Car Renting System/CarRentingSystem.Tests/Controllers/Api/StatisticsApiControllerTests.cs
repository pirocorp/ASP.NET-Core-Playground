namespace CarRentingSystem.Tests.Controllers.Api
{
    using System.Threading.Tasks;

    using CarRentingSystem.Controllers.Api;
    using CarRentingSystem.Services.Models.Statistics;
    using CarRentingSystem.Tests.Mocks;

    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class StatisticsApiControllerTests
    {
        [Fact]
        public async Task GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var actionResult = await statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(actionResult);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var statisticsResult = Assert.IsType<StatisticsServiceModel>(objectResult.Value);

            Assert.NotNull(statisticsResult);

            Assert.Equal(5, statisticsResult?.TotalCars);
            Assert.Equal(10, statisticsResult?.TotalRents);
            Assert.Equal(15, statisticsResult?.TotalUsers);
        }
    }
}
