namespace RecipeApplication.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;

    using RecipeApplication.Data;

    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private SqliteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            this._connection = new SqliteConnection("DataSource=:memory:");
            this._connection.Open();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.AddDbContext<AppDbContext>(opts =>
                {
                    opts.UseSqlite(this._connection);
                });
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host =  base.CreateHost(builder);

            using var scope = host.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            return host;
        }
    }
}
