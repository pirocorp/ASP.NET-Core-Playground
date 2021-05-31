namespace BackgroundServiceCache
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class ExchangeRatesHostedService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ExchangeRatesCache _cache;
        private readonly ILogger<ExchangeRatesHostedService> _logger;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(10);

        public ExchangeRatesHostedService(
            ExchangeRatesCache cache,
            IServiceProvider provider,
            ILogger<ExchangeRatesHostedService> logger)
        {
            this._cache = cache;
            this._provider = provider;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("Fetching latest rates");
                var client = this._provider.GetRequiredService<ExchangeRatesClient>();
                var latest = await client.GetLatestRatesAsync();
                this._cache.SetRates(latest);
                this._logger.LogInformation("Latest rates updated");

                await Task.Delay(this._refreshInterval, stoppingToken);
            }
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        using (IServiceScope scope = _provider.CreateScope())
        //        {
        //            var scopedProvider = scope.ServiceProvider;

        //            var client = scope.ServiceProvider
        //                .GetRequiredService<ExchangeRatesClient>();

        //            var context = scope.ServiceProvider
        //                .GetRequiredService<AppDbContext>();

        //            var rates = await client.GetLatestRatesAsync();

        //            context.Add(rates);
        //            await context.SaveChanges(rates);
        //        }

        //        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        //    }
        //}
    }
}
