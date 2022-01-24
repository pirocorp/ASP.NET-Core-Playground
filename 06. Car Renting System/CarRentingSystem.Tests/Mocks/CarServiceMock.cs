namespace CarRentingSystem.Tests.Mocks
{
    using CarRentingSystem.Services.Cars;

    using Moq;

    public static class CarServiceMock
    {
        public static ICarService Instance
        {
            get
            {
                var carServiceMock = new Mock<ICarService>();

                return carServiceMock.Object;
            }
        }
    }
}
