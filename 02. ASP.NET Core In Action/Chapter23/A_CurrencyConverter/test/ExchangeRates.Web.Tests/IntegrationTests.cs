namespace ExchangeRates.Web.Tests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using System.Net.Http;
    using System.Threading.Tasks;

    using Xunit;

    /// <summary>
    /// Integration tests that use the application defined in ExcahngeRates.Web. (See section 22.5.2 and 22.5.3)
    /// </summary>
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;

        public IntegrationTests(WebApplicationFactory<Startup> fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            HttpClient client = this._fixture.CreateClient();

            // Act
            var response = await client.GetAsync("/ping");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("pong", content);
        }

        [Fact]
        public async Task HomePageReturnsHtml()
        {
            HttpClient client = this._fixture.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html", response.Content.Headers.ContentType.MediaType);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Enter values and click convert", content);
        }

        [Fact]
        public async Task ConvertReturnsExpectedValue()
        {
            var customFactory = this._fixture.WithWebHostBuilder(hostBuilder =>
            {
                hostBuilder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<ICurrencyConverter>();
                    services.AddSingleton<ICurrencyConverter, StubCurrencyConverter>();
                });
            });

            HttpClient client = customFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/currency");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("3", content);
        }
    }
}
