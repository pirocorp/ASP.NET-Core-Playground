namespace ExchangeRates.Web
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class StatusMiddleware
    {
        private readonly RequestDelegate _next;

        public StatusMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/ping"))
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("pong");
                return;
            }

            await this._next(context);
        }
    }
}
