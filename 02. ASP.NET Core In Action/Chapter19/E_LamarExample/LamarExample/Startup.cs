namespace LamarExample
{
    using Lamar;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ServiceRegistry services)
        {
            // Supports ASP.Net Core DI abstractions
            services.AddAuthorization();
            services.AddControllers()
                .AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LamarExample", Version = "v1" });
            });

            // Also exposes Lamar specific registrations
            // and functionality
            services.Scan(s =>
            {
                // Automatically register services that follow default conventions, e.g.
                // PurchasingService/IPurchasingService
                // ConcreteService (concrete types can always be resolved)
                // Typically, you will have a lot of Service/IService pairs in your app
                s.AssemblyContainingType(typeof(Startup));
                s.WithDefaultConventions();
                // Register all of the implementations of IGamingService
                // CrosswordService
                // SudokuService
                s.AddAllTypesOf<IGamingService>();
                // Register all non-generic implementations of IValidatior<T> (UserModelValidator)
                s.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
            });


            // When a ILeaderboard<T> is requested, use Leaderboard<T>
            // Equivalent to:
            // services.AddTransient(typeof(ILeaderboard<>), typeof(Leaderboard<>));
            services.For(typeof(ILeaderboard<>)).Use(typeof(Leaderboard<>));
            // When an IUnitOfWork<T> is requested, run the lambda
            // Also, has a "scoped" lifetime, instead of the default "transient" lifetime
            // Equivalent to:
            //services.AddScoped<IUnitOfWork>(_ => new UnitOfWork(3));
            services.For<IUnitOfWork>().Use(_ => new UnitOfWork(3)).Scoped();
            // For a given T, when an IValidator<T> is requested, 
            // but there are no non-generic implementations of IValidator<T>
            // Use DefaultValidator<T> instead
            // No equivalent using the built-in container
            services.For(typeof(IValidator<>)).Add(typeof(DefaultValidator<>));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // This method is optional, you can remove it entirely if you wish
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LamarExample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
