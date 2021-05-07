namespace CustomEndpoint
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class PingPongMiddleware
    {
        public PingPongMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("pong");
        }
    }
}