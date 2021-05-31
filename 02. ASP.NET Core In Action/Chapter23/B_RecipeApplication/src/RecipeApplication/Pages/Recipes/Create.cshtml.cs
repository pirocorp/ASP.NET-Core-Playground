namespace RecipeApplication.Pages.Recipes
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using RecipeApplication.Data;
    using RecipeApplication.Models;

    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRecipeCommand Input { get; set; }
        private readonly RecipeService _service;
        private readonly UserManager<ApplicationUser> _userService;

        public CreateModel(RecipeService service, UserManager<ApplicationUser> userService)
        {
            this._service = service;
            this._userService = userService;
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
                    var appUser = await this._userService.GetUserAsync(this.User);
                    var id = await this._service.CreateRecipe(this.Input, appUser);
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