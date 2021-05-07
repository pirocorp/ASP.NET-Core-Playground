namespace CustomEndpoint
{
    using CustomEndpoint.Data;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                // Build the pipeline in-line
                var endpoint = endpoints
                    .CreateApplicationBuilder()
                    .UseMiddleware<PingPongMiddleware>()
                    .Build();

                endpoints.Map("/pip-pip", endpoint);

                // See EndpointRouteBuilderExtensions for the definition of these methods
                endpoints.MapVersion("/version");
                endpoints.MapPingPong("/ping");

                //endpoints.MapGet("/ping", (HttpContext ctx) => 
                //    ctx.Response.WriteAsync("pong"));

                // Example of using route parameters in endpoint routes
                endpoints
                    .MapMiddleware<CalculatorMiddleware>("/add/{a}/{b}")
                    .WithDisplayName("Calculator");

                // Requires authorization (must be logged in)
                endpoints
                    .MapHealthChecks("/healthz")
                    .RequireAuthorization();

                endpoints.MapGet("/isEven/{value:int}", context => {
                    var value = int.Parse((string)context.GetRouteValue("value"));
                    var isEven = value % 2 == 0;
                    return context.Response.WriteAsync(isEven ? "even" : "odd");
                });

                endpoints.MapGet("/status", async context => {
                    var status = new { Running = true };
                    await context.Response.WriteAsJsonAsync(status);
                });

                endpoints.MapPost("/echo", async context => {
                    var json = await context.Request.ReadFromJsonAsync<MyCustomType>();
                    await context.Response.WriteAsJsonAsync(json);
                });
            });
        }
    }
    public class MyCustomType
    { }
}
