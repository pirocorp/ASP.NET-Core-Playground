namespace RecipeApplication.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly RecipeService _service;

        public IEnumerable<RecipeSummaryViewModel> Recipes { get; private set; }

        public IndexModel(RecipeService service)
        {
            this._service = service;
        }

        public async Task OnGet()
        {
            this.Recipes = await this._service.GetRecipes();

        }
    }
}
