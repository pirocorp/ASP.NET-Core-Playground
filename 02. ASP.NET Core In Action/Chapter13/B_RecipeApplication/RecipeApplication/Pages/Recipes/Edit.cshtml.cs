namespace RecipeApplication.Pages.Recipes
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    public class EditModel : PageModel
    {
        private readonly RecipeService _service;

        public EditModel(RecipeService service)
        {
            this._service = service;
        }

        [BindProperty]
        public UpdateRecipeCommand Input { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
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