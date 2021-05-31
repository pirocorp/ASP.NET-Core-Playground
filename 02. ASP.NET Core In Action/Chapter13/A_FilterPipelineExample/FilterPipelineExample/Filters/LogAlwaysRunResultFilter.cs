namespace FilterPipelineExample.Filters
{
    using System;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class LogAlwaysRunResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Executing IAlwaysRunResultFilter.OnResultExecuting");
            //context.Cancel = true;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Executing IAlwaysRunResultFilter.OnResultExecuted");
        }
    }
}
