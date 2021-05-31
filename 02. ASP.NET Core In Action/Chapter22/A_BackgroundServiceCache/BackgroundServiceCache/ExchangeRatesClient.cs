namespace BackgroundServiceCache
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExchangeRatesClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExchangeRatesClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ExchangeRates> GetLatestRatesAsync()
        {
            return await this._httpClient.GetFromJsonAsync<ExchangeRates>("latest", this._serializerOptions);
        }
    }
}
