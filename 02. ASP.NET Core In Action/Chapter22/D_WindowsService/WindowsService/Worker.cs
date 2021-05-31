namespace WindowsService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using WindowsService.Data;

    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(10);
        private readonly ILogger<Worker> _logger;

        public Worker(
            IServiceProvider provider,
            ILogger<Worker> logger)
        {
            this._provider = provider;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = this._provider.CreateScope())
                {
                    this._logger.LogInformation("Fetching latest rates");
                    var client = this._provider.GetRequiredService<ExchangeRatesClient>();
                    var latestRates = await client.GetLatestRatesAsync();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Add(latestRates);
                    await context.SaveChangesAsync();
                    this._logger.LogInformation("Latest rates updated");
                }

                await Task.Delay(this._refreshInterval, stoppingToken);
            }
        }
    }
}
