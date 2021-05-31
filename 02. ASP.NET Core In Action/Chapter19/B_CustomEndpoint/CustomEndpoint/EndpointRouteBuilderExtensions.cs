namespace CustomEndpoint
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapVersion(this IEndpointRouteBuilder endpoints, string pattern)
            => MapMiddleware<VersionMiddleware>(endpoints, pattern)
                .WithDisplayName("Version number");

        public static IEndpointConventionBuilder MapPingPong(this IEndpointRouteBuilder endpoints, string pattern)
            => MapMiddleware<PingPongMiddleware>(endpoints, pattern)
                .WithDisplayName("Ping-pong");

        public static IEndpointConventionBuilder MapMiddleware<T>(this IEndpointRouteBuilder endpoints, string pattern)
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<T>()
                .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}
