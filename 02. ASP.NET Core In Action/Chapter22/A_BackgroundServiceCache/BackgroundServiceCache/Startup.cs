namespace BackgroundServiceCache
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Net.Http.Headers;
    using Microsoft.OpenApi.Models;

    using Polly;

    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackgroundServiceCache", Version = "v1" });
            });

            services.AddHttpClient<ExchangeRatesClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.exchangeratesapi.io");
                client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "ExchangeRateViewer");
            })
            .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            services.AddSingleton<ExchangeRatesCache>();
            services.AddHostedService<ExchangeRatesHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackgroundServiceCache v1"));
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
