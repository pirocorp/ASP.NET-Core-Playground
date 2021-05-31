namespace RecipeApplication.Controllers
{
    using System.Threading.Tasks;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using RecipeApplication.Models;

    [Route("api/recipe")]
    [ValidateModel, HandleException, FeatureEnabled(IsEnabled = true)]
    public class RecipeApiController : ControllerBase
    {
        private readonly RecipeService _service;

        public RecipeApiController(RecipeService service)
        {
            this._service = service;
        }

        [HttpGet("{id}"), EnsureRecipeExists, AddLastModifiedHeader]
        public async Task<IActionResult> Get(int id)
        {
            var detail = await this._service.GetRecipeDetail(id);
            return this.Ok(detail);

        }

        [HttpPost("{id}"), EnsureRecipeExists, RequireIpAddress]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateRecipeCommand command)
        {
            await this._service.UpdateRecipe(command);
            return this.Ok();
        }
    }
}
