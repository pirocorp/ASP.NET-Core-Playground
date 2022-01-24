namespace CarRentingSystem.Tests.Services
{
    using System.Threading.Tasks;

    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Services.Dealers;
    using CarRentingSystem.Tests.Mocks;

    using Xunit;

    public class DealerServiceTests
    {
        private const string UserId = "MockUserId";

        [Fact]
        public async Task IsDealerShouldReturnTrueWhenUserIsDealer()
        {
            // Arrange
            var dealerService = await GetDealerService(UserId);

            // Act
            var result = await dealerService.UserIsDealer(UserId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsDealerShouldReturnFalseWhenUserIsNotDealer()
        {
            // Arrange
            var dealerService = await GetDealerService(UserId);

            // Act
            var result = await dealerService.UserIsDealer("InvalidUserId");

            // Assert
            Assert.False(result);
        }

        private static async Task<IDealerService> GetDealerService(string userId)
        {
            var data = DatabaseMock.Instance;

            await data.Dealers.AddAsync(new Dealer("Mock Dealer", "+1 111 111 111", userId));
            await data.SaveChangesAsync();

            var dealerService = new DealerService(data);

            return dealerService;
        }
    }
}
