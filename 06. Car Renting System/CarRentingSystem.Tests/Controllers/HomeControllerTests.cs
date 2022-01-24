namespace CarRentingSystem.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Controllers;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Cars;
    using CarRentingSystem.Tests.Mocks;

    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(CarServiceMock.Instance);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var cars = new List<Car>()
            {
                new Car("4", "8", "12", "16", 20, 24, 28),
                new Car("4", "8", "12", "16", 20, 24, 28),
                new Car("4", "8", "12", "16", 20, 24, 28),
                new Car("1", "5", "9", "13", 17, 21, 25),
                new Car("2", "6", "10", "14", 18, 22, 26),
                new Car("3", "7", "11", "15", 19, 23, 27),
                new Car("4", "8", "12", "16", 20, 24, 28),
            };

            await data.Cars.AddRangeAsync(cars);

            await data.SaveChangesAsync();

            var carService = new CarService(data, mapper);
            var homeController = new HomeController(carService);

            // Act
            var result = await homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);
            Assert.Equal(WebConstants.CarouselSize, indexViewModel.Cars.Count());
        }
    }
}
