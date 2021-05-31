namespace RecipeApplication.Pages.Recipes
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;


    public class ViewModel : PageModel
    {
        private readonly RecipeService _service;
        private readonly IAuthorizationService _authService;

        public ViewModel(RecipeService service, IAuthorizationService authService)
        {
            this._service = service;
            this._authService = authService;
        }

        public RecipeDetailViewModel Recipe { get; set; }

        public bool CanEditRecipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            this.Recipe = await this._service.GetRecipeDetail(id);
            if (this.Recipe is null)
            {
                // If id is not for a valid Recipe, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return this.NotFound();
            }
            var recipe = await this._service.GetRecipe(id);
            var isAuthorised = await this._authService.AuthorizeAsync(this.User, recipe, "CanManageRecipe");
            this.CanEditRecipe = isAuthorised.Succeeded;
            return this.Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var recipe = await this._service.GetRecipe(id);
            var authResult = await this._authService.AuthorizeAsync(this.User, recipe, "CanManageRecipe");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            await this._service.DeleteRecipe(id);

            return this.RedirectToPage("/Index");
        }
    }
}