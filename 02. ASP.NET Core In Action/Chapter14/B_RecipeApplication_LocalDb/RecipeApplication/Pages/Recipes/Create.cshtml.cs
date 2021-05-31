namespace RecipeApplication.Pages.Recipes
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Models;

    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRecipeCommand Input { get; set; }
        private readonly RecipeService _service;

        public CreateModel(RecipeService service)
        {
            this._service = service;
        }
        
        public void OnGet()
        {
            this.Input = new CreateRecipeCommand();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var id = await this._service.CreateRecipe(this.Input);
                    return this.RedirectToPage("View", new { id = id });
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