namespace CarRentingSystem.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Controllers;
    using CarRentingSystem.Services.Cars;
    using CarRentingSystem.Services.Models.Cars;
    using CarRentingSystem.Tests.Mocks;

    using Microsoft.AspNetCore.Mvc;

    using Xunit;

    using static CarRentingSystem.Tests.Data.Cars;
    using static CarRentingSystem.WebConstants.Cache;

    public class HomeControllerTests
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(
                CarServiceMock.Instance,
                MemoryCacheMock.Instance);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var cars = TenPublicCars();

            await data.Cars.AddRangeAsync(cars);
            await data.SaveChangesAsync();

            var carService = new CarService(data, mapper);
            var memoryCache = MemoryCacheMock.Instance;

            var homeController = new HomeController(carService, memoryCache);

            // Act
            var result = await homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;

            var indexViewModel = Assert.IsAssignableFrom<IEnumerable<CarLatestServiceModel>>(model);
            Assert.Equal(WebConstants.CarouselSize, indexViewModel.Count());

            Assert.True(memoryCache.TryGetValue(LatestCarsCacheKey, out var carsValue));
            Assert.Equal(indexViewModel, carsValue);
        }
    }
}
