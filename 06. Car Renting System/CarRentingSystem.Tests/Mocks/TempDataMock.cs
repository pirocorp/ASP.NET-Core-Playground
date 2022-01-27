namespace CarRentingSystem.Tests.Mocks
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    public static class TempDataMock
    {
        public static ITempDataDictionary Instance
            => new TempDataDictionary(
                new DefaultHttpContext(),
                new Mock<ITempDataProvider>().Object);
    }
}
