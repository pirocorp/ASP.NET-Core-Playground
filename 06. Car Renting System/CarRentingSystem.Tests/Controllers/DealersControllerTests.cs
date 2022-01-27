namespace CarRentingSystem.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CarRentingSystem.Controllers;
    using CarRentingSystem.Models.Dealers;
    using CarRentingSystem.Services.Dealers;
    using CarRentingSystem.Tests.Extensions;
    using CarRentingSystem.Tests.Mocks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Xunit;

    using static WebConstants;

    public class DealersControllerTests
    {
        [Fact]
        public void CreateShouldBeForAuthorizedUsersAndReturnView()
        {
            // Arrange
            var dealersController = new DealersController(DealerServiceMock.Instance);

            var methodIsAuthorized = dealersController
                    .MethodIsForAuthorizedRequestsOnly(nameof(DealersController.Create), Array.Empty<Type>());

            // Act
            var result = dealersController.Create();

            // Assert
            Assert.True(methodIsAuthorized);
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData("userId", "DealerName", "DealerPhoneNumber")]
        public async Task PostCreateShouldBeForAuthorizedUsersAndReturnRedirect(
            string userId,
            string dealerName,
            string dealerPhoneNumber)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var dealerService = new DealerService(data);
            var dealersController = new DealersController(dealerService);

            dealersController.WithUserIdentifier(userId);
            dealersController.TempData = TempDataMock.Instance;

            var methodName = nameof(DealersController.Create);
            var methodParameters = new[]
            {
                typeof(BecomeDealerFormModel),
            };

            var methodIsAuthorized = dealersController
                .MethodIsForAuthorizedRequestsOnly(methodName, methodParameters);

            var methodIsHttpPost = dealersController
                .MethodHasAttribute<HttpPostAttribute>(methodName, methodParameters);

            var dealerModel = new BecomeDealerFormModel()
            {
                Name = dealerName,
                PhoneNumber = dealerPhoneNumber,
            };

            // Act
            var result = await dealersController.Create(dealerModel);

            // Assert
            Assert.True(methodIsAuthorized);
            Assert.True(methodIsHttpPost);
            Assert.IsType<RedirectToActionResult>(result);

            var dealer = await data.Dealers.FirstAsync();

            Assert.NotNull(dealer);
            Assert.Equal(userId, dealer.UserId);
            Assert.Equal(dealerModel.Name, dealer.Name);
            Assert.Equal(dealerModel.PhoneNumber, dealer.PhoneNumber);
            Assert.True(dealersController.TempData.Keys.Contains(GlobalMessageKey));
        }
    }
}
