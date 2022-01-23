namespace CarRentingSystem.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static Areas.Admin.AdminConstants;

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
            var serviceProvider = serviceScope.ServiceProvider;

            SeedCategories(serviceProvider);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<CarRentingDbContext>();

            if (dbContext.Categories.Any())
            {
                return;
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
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole(AdministratorRoleName);
                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@crs.com";
                    const string adminPassword = "123456";

                    var user = new User()
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin",
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
