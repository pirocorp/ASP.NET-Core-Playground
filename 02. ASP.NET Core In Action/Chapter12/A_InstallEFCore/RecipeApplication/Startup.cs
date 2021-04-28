namespace RecipeApplication
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            // Update User Secrets/appsettings.json with the connection string for your database
            //
            // e.g. for SQLite:
            // Data Source=recipe_app.db;
            //
            // or for LocalDB:
            // (localdb)\\mssqllocaldb;Database=RecipeApplication;Trusted_Connection=True;MultipleActiveResultSets=true

            var connString = this.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(
                // If you're using SQL Server or Local DB, use the following line:
                options => options.UseSqlServer(connString)
                // If you're using SQLite, use the following line instead:
                //options => options.UseSqlite(connString)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
