namespace RecipeApplication.Pages.Recipes
{
    using System.Threading.Tasks;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    [PageEnsureRecipeExists]
    // PageModel implements IPageFilter and IAsyncPageFilter so if filter is used only in one PageModel
    // simply override the necessary Page method. 
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
            return this.Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this._service.DeleteRecipe(id);

            return this.RedirectToPage("/Index");
        }
    }
}