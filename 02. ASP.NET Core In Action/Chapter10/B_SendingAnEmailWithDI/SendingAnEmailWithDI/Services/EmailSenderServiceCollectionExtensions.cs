namespace SendingAnEmailWithoutDI.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public static class EmailSenderServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<NetworkClient>();
            services.AddScoped<MessageFactory>();
            services.AddSingleton(
                new EmailServerSettings
                (
                    host: "smtp.server.com",
                    port: 25
                ));
            return services;
        }
    }
}
