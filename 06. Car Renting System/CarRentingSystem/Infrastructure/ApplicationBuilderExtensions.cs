namespace CarRentingSystem.Infrastructure
{
    using System.Linq;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<CarRentingDbContext>();
            dbContext.Database.Migrate();

            return app;
        }

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<CarRentingDbContext>();

            if (dbContext.Categories.Any())
            {
                return app;
            }

            dbContext.Categories.AddRange(
                new Category("Mini"),
                new Category("Economy"),
                new Category("Midsize"),
                new Category("Large"),
                new Category("SUV"),
                new Category("Van"),
                new Category("Luxury/Cabrio"));

            dbContext.SaveChanges();

            return app;
        }
    }
}
