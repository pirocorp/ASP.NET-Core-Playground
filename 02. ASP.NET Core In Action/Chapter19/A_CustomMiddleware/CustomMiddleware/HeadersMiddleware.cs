namespace CustomMiddleware
{
    using Microsoft.AspNetCore.Http;

    using System.Threading.Tasks;

    public class HeadersMiddleware
    {
        private readonly RequestDelegate _next; // Represents the rest of the middleware pipeline

        public HeadersMiddleware(RequestDelegate next) // All dependencies injected in constructor must be Singletons
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context) // For Transient and Singleton dependencies use Invoke method
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                return Task.CompletedTask;
            });

            await this._next(context);
        }
    }
}
