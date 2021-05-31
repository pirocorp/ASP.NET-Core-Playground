namespace ExchangeRateViewer
{
    using Microsoft.Extensions.Options;

    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApiKeyMessageHandler : DelegatingHandler
    {
        private readonly ExchangeRateApiSettings _settings;

        public ApiKeyMessageHandler(IOptions<ExchangeRateApiSettings> settings)
        {
            this._settings = settings.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-API-KEY", this._settings.ApiKey);

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
