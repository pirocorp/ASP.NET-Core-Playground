using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RecipeApplication.Areas.Identity.IdentityHostingStartup))]
namespace RecipeApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}