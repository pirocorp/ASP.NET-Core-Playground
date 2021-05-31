namespace RecipeApplication.ViewComponents
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using RecipeApplication.Data;
    using RecipeApplication.Models;

    using System.Linq;
    using System.Threading.Tasks;

    public class MyRecipesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyRecipesViewComponent(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int numberOfRecipes)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View("Unauthenticated");
            }

            var userId = this._userManager.GetUserId(this.HttpContext.User);

            var recipes = await this._context.Recipes
                .Where(x => x.CreatedById == userId)
                .OrderBy(x => x.LastModified)
                .Take(numberOfRecipes)
                .Select(x => new RecipeSummaryViewModel
                {
                    Id = x.RecipeId,
                    Name = x.Name,
                })
                .ToListAsync();

            return this.View(recipes);
        }
    }
}
