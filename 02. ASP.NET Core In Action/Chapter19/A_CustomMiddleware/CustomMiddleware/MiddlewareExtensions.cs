namespace CustomMiddleware
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePingMiddleware(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/ping"))
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("pong");
                }
                else
                {
                    await next.Invoke();
                }
            });
        }

        public static IApplicationBuilder UseCalculatorMiddleware(this IApplicationBuilder app)
        {
            return app.Map("/add", branch =>
            {
                branch.Run(async context =>
                {
                    var query = context.Request.Query;
                    if (int.TryParse(query["a"], out var a) &&
                       int.TryParse(query["b"], out var b))
                    {
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync($"{a} + {b} = {a + b}");
                    }
                    else
                    {
                        //bad request
                        context.Response.StatusCode = 400;
                    }
                });
            });
        }
    }
}
