namespace RecipeApplication.Filters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class PageEnsureRecipeExistsAttribute : TypeFilterAttribute
    {
        public PageEnsureRecipeExistsAttribute()
            : base(typeof(PageEnsureRecipeExistsFilter))
        { }

        private class PageEnsureRecipeExistsFilter : IAsyncPageFilter
        {
            private readonly RecipeService service;

            public PageEnsureRecipeExistsFilter(RecipeService service)
            {
                this.service = service;
            }

            public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
            {
                await Task.CompletedTask;
            }

            public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
            {
                var recipeId = (int)context.HandlerArguments["id"];

                if (!await this.service.DoesRecipeExistAsync(recipeId))
                {
                    context.Result = new NotFoundResult();

                    return;
                }

                await next();
            }
        }
    }
}
