using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ReplacingDefaultConfigurationProviders
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddAppConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // Custom App Configuration Overrides some of default configurations
        private static void AddAppConfiguration(
            HostBuilderContext hostingContext,
            IConfigurationBuilder config)
        {
			var env = hostingContext.HostingEnvironment;
			
			if(env.IsDevelopment())
			{
				config.AddUserSecrets<Startup>();
			}
			
            config.Sources.Clear();            
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config.AddJsonFile("extrasettings.json", optional: false, reloadOnChange: true);
        }
    }
}
