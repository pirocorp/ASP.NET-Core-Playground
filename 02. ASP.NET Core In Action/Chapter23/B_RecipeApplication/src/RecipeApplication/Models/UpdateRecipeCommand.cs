namespace RecipeApplication.Models
{
    using System;

    using RecipeApplication.Data;

    public class UpdateRecipeCommand : EditRecipeBase
    {
        public int Id { get; set; }
        
        public void UpdateRecipe(Recipe recipe)
        {
            recipe.Name = this.Name;
            recipe.TimeToCook = new TimeSpan(this.TimeToCookHrs, this.TimeToCookMins, 0);
            recipe.Method = this.Method;
            recipe.IsVegetarian = this.IsVegetarian;
            recipe.IsVegan = this.IsVegan;
            recipe.LastModified = DateTimeOffset.UtcNow;
        }
    }
}
