namespace ConfigureOptionsExample
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CurrencyOptions>(this.Configuration.GetSection("Currencies"));

            services.Configure<CurrencyOptions>(options =>
                options.Currencies = new string[] { "GBP", "USD", "EUR" });

            // This will configure CurrencyOptions just before DI Container inject it.
            // ConfigureCurrencyOptions will run after CurrencyOptions binding to configuration.
            services.AddSingleton<IConfigureOptions<CurrencyOptions>, ConfigureCurrencyOptions>(); 
            services.AddSingleton<ICurrencyProvider, CurrencyProvider>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfigureOptionsExample", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConfigureOptionsExample v1"));
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
