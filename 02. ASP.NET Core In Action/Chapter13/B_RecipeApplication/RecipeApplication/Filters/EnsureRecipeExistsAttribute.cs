namespace RecipeApplication.Filters
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class EnsureRecipeExistsAttribute : TypeFilterAttribute
    {
        public EnsureRecipeExistsAttribute()
            : base(typeof(EnsureRecipeExistsFilter))
        { }

        private class EnsureRecipeExistsFilter : IAsyncActionFilter
        {
            private readonly RecipeService _service;

            public EnsureRecipeExistsFilter(RecipeService service)
            {
                this._service = service;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var recipeId = (int)context.ActionArguments["id"];

                if (!await this._service.DoesRecipeExistAsync(recipeId))
                {
                    context.Result = new NotFoundResult();

                    return;
                }

                await next();
            }
        }
    }
}
