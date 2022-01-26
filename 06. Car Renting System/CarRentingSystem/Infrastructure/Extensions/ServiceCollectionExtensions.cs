namespace CarRentingSystem.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLowercaseRouting(this IServiceCollection services)
            => services.AddRouting(routing => routing.LowercaseUrls = true);
    }
}
