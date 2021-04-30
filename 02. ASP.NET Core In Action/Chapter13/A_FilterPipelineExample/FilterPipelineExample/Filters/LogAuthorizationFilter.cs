namespace FilterPipelineExample.Filters
{
    using System;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class LogAuthorizationFilter : Attribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("Executing IAuthorizationFilter.OnAuthorization");
            //context.Result = new ContentResult()
            //{
            //    Content = "IAuthorizationFilter - Short-circuiting ",
            //};
        }
    }
}
