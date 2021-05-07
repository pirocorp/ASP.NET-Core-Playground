namespace RecipeApplication.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using RecipeApplication.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly RecipeService _service;
        private readonly ILogger<IndexModel> _log;

        public ICollection<RecipeSummaryViewModel> Recipes { get; private set; }

        public IndexModel(RecipeService service, ILogger<IndexModel> log)
        {
            this._service = service;
            this._log = log;
        }

        public async Task OnGet()
        {
            this.Recipes = await this._service.GetRecipes();
            this._log.LogInformation("Loaded {RecipeCount} recipes", this.Recipes.Count);
        }
    }
}
