namespace RecipeApplication.Pages.Recipes
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    [Authorize]
    public class EditModel : PageModel
    {
        private readonly RecipeService _service;
        private readonly IAuthorizationService _authService;

        public EditModel(RecipeService service, IAuthorizationService authService)
        {
            this._service = service;
            this._authService = authService;
        }

        [BindProperty]
        public UpdateRecipeCommand Input { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var recipe = await this._service.GetRecipe(id);
            var authResult = await this._authService.AuthorizeAsync(this.User, recipe, "CanManageRecipe");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            this.Input = await this._service.GetRecipeForUpdate(id);
            if (this.Input is null)
            {
                // If id is not for a valid Recipe, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return this.NotFound();
            }
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var recipe = await this._service.GetRecipe(this.Input.Id);
                var authResult = await this._authService.AuthorizeAsync(this.User, recipe, "CanManageRecipe");
                if (!authResult.Succeeded)
                {
                    return new ForbidResult();
                }

                if (this.ModelState.IsValid)
                {
                    await this._service.UpdateRecipe(this.Input);
                    return this.RedirectToPage("View", new { id = this.Input.Id });
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                this.ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the recipe"
                    );
            }

            //If we got to here, something went wrong
            return this.Page();
        }
    }
}