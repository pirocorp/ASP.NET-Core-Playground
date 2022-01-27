namespace CarRentingSystem.Tests.Mocks
{
    using CarRentingSystem.Services.Dealers;

    using Moq;

    public static class DealerServiceMock
    {
        public static IDealerService Instance => new Mock<IDealerService>().Object;
    }
}
