namespace QuartzHostedService
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Quartz;

    using QuartzHostedService.Data;
    
    [DisallowConcurrentExecution]
    public class UpdateExchangeRatesJob : IJob
    {
        private readonly ILogger<UpdateExchangeRatesJob> _logger;
        private readonly ExchangeRatesClient _httpClient;
        private readonly AppDbContext _dbContext;

        public UpdateExchangeRatesJob(ILogger<UpdateExchangeRatesJob> logger, ExchangeRatesClient httpClient, AppDbContext dbContext)
        {
            this._logger = logger;
            this._httpClient = httpClient;
            this._dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Fetching latest rates");

            var latestRates = await this._httpClient.GetLatestRatesAsync();

            this._dbContext.Add(latestRates);
            await this._dbContext.SaveChangesAsync();

            this._logger.LogInformation("Latest rates updated");
        }
    }

}
