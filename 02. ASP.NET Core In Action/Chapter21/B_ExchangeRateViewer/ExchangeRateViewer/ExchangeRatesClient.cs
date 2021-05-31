namespace ExchangeRateViewer
{
    using Microsoft.Net.Http.Headers;

    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExchangeRatesClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExchangeRatesClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri("https://api.exchangeratesapi.io");
            this._httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "ExchangeRateViewer");
        }

        public async Task<ExchangeRates> GetLatestRatesAsync()
        {
            var result = await this._httpClient.GetAsync("latest");
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ExchangeRates>(stream, this._serializerOptions);

            // or, using System.Net.Http.Json

            //return await _httpClient.GetFromJsonAsync<ExchangeRates>("latest", _serializerOptions);
        }
    }
}
