namespace ExchangeRates.Web.Tests
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;

    using System.Net.Http;
    using System.Threading.Tasks;

    using Xunit;

    /// <summary>
    /// Integration tests for the <see cref="StatusMiddleware"/> using TestHost. (See section 22.5.1)
    /// </summary>
    public class StatusMiddlewareTestHostTests
    {
        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            var hostBuilder = new HostBuilder()
              .ConfigureWebHost(webHost =>
              {
                  webHost.UseTestServer();
                  webHost.Configure(app => app.UseMiddleware<StatusMiddleware>());
              });

            IHost host = await hostBuilder.StartAsync();
            HttpClient client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/ping");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("pong", content);
        }
    }
}
