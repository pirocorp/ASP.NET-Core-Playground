namespace BookShop.Api.Infrastructure.Extensions
{
    using BookShop.Services;
    using Common.Mapping;

    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all services which are in assembly of IService interface as transient into IoC Container
        /// </summary>
        /// <param name="services">IoC Container</param>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                ?.GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name.Equals($"I{t.Name}")))
                .Select(t => new
                {
                    Interface = t.GetInterface($"{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }

        /// <summary>
        /// Add AutoMapper in IServiceCollection (IoC Container).
        /// </summary>
        /// <param name="services">IServiceCollection (IoC Container).</param>
        /// <returns>IServiceCollection (IoC Container) with added AutoMapper.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(AppDomain.CurrentDomain.GetAssemblies());  // Get all mapping configurations and build automapper instance

            services.AddSingleton(AutoMapperConfig.MapperInstance); // Add mapper instance as singleton to IoC Container, it can be injected as IMapper

            return services;
        }
    }
}
