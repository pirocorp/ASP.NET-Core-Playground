namespace FilterPipelineExample
{
    using FilterPipelineExample.Filters;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
                //.AddMvcOptions(options =>
                //{
                //    options.Filters.Add(new GlobalLogAsyncActionFilter()); // won't apply to page
                //    options.Filters.Add(new GlobalLogAsyncPageFilter()); // won't apply to action
                //    options.Filters.Add(new GlobalLogAsyncAuthorizationFilter());
                //    options.Filters.Add(new GlobalLogAsyncExceptionFilter());
                //    options.Filters.Add(new GlobalLogAsyncResourceFilter());
                //    options.Filters.Add(new GlobalLogAsyncResultFilter());
                //    options.Filters.Add(new GlobalLogAsyncAlwaysRunResultFilter());
                //})
                

            services.AddControllers(options =>
            {
                options.Filters.Add(new GlobalLogAsyncActionFilter()); // won't apply to page
                options.Filters.Add(new GlobalLogAsyncPageFilter()); // won't apply to action
                options.Filters.Add(new GlobalLogAsyncAuthorizationFilter());
                options.Filters.Add(new GlobalLogAsyncExceptionFilter());
                options.Filters.Add(new GlobalLogAsyncResourceFilter());
                options.Filters.Add(new GlobalLogAsyncResultFilter());
                options.Filters.Add(new GlobalLogAsyncAlwaysRunResultFilter());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
