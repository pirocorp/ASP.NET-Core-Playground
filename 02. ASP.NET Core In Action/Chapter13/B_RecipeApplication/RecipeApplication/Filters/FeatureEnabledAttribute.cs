namespace RecipeApplication.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class FeatureEnabledAttribute : Attribute, IResourceFilter
    {
        public bool IsEnabled { get; set; }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!this.IsEnabled)
            {
                context.Result = new BadRequestResult();
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context) { }
    }
}
