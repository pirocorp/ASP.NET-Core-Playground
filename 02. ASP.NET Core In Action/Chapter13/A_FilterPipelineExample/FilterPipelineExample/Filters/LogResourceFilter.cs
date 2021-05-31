﻿namespace FilterPipelineExample.Filters
{
    using System;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class LogResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Executing IResourceFilter.OnResourceExecuting");
            //context.Result = new ContentResult()
            //{
            //    Content = "IResourceFilter - Short-circuiting ",
            //};
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"Executing IResourceFilter.OnResourceExecuted: cancelled {context.Canceled}");
        }
    }
}
