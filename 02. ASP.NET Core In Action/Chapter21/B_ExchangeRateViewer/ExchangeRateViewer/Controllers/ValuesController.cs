namespace ExchangeRateViewer.Controllers
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ExchangeRatesClient _ratesClient;
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.exchangeratesapi.io"),
        };
        public ValuesController(IHttpClientFactory clientFactory, ExchangeRatesClient ratesClient)
        {
            this._clientFactory = clientFactory;
            this._ratesClient = ratesClient;
        }

        /// <summary>
        /// Send requets using a singleton HttpClient instance
        /// </summary>
        [HttpGet("httpclient")]
        public async Task<ActionResult<ExchangeRates>> HttpClientAsync()
        {
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "ExchangeRateViewer");
            var result = await _httpClient.GetAsync("latest");
            result.EnsureSuccessStatusCode();

            // Return results as json.
            var stream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ExchangeRates>(stream, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Send requets using IHttpClientFactory
        /// </summary>
        [HttpGet("httpclientfactory")]
        public async Task<ActionResult<ExchangeRates>> HttpClientFactoryAsync()
        {
            var httpClient = this._clientFactory.CreateClient();
            
            httpClient.BaseAddress = new Uri("https://api.exchangeratesapi.io");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "ExchangeRateViewer");

            var result = await httpClient.GetAsync("latest");
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ExchangeRates>(stream, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Send requets using named client
        /// </summary>
        [HttpGet("namedclient")]
        public async Task<ActionResult<ExchangeRates>> NamedClientAsync()
        {
            var httpClient = this._clientFactory.CreateClient("rates");

            var result = await httpClient.GetAsync("latest");
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ExchangeRates>(stream, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Send requets using typed client
        /// </summary>
        [HttpGet("typedclient")]
        public async Task<ActionResult<ExchangeRates>> TypedClientAsync()
        {
            return await this._ratesClient.GetLatestRatesAsync();
        }
    }
}
