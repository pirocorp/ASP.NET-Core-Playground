namespace WindowsService
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WindowsService.Data;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlite(hostContext.Configuration.GetConnectionString("SqlLiteConnection"))
                    );

                    services.AddHttpClient<ExchangeRatesClient>();
                })
            .UseWindowsService();
    }
}
