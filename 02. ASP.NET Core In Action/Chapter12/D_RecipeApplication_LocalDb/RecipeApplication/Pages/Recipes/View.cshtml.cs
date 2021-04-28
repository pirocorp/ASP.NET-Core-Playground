namespace RecipeApplication.Pages.Recipes
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    public class ViewModel : PageModel
    {
        private readonly RecipeService _service;

        public ViewModel(RecipeService service)
        {
            this._service = service;
        }

        public RecipeDetailViewModel Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            this.Recipe = await this._service.GetRecipeDetail(id);
            if (this.Recipe is null)
            {
                // If id is not for a valid Recipe, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return this.NotFound();
            }
            return this.Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this._service.DeleteRecipe(id);

            return this.RedirectToPage("/Index");
        }
    }
}