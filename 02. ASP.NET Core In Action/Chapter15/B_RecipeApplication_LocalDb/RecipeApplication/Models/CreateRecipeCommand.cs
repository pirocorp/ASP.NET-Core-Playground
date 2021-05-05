namespace RecipeApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RecipeApplication.Data;

    public class CreateRecipeCommand : EditRecipeBase
    {
        public IList<CreateIngredientCommand> Ingredients { get; set; } = new List<CreateIngredientCommand>();

        public Recipe ToRecipe(ApplicationUser createdBy)
        {
            return new Recipe
            {
                Name = this.Name,
                TimeToCook = new TimeSpan(this.TimeToCookHrs, this.TimeToCookMins, 0),
                Method = this.Method,
                IsVegetarian = this.IsVegetarian,
                IsVegan = this.IsVegan,
                Ingredients = this.Ingredients?.Select(x=>x.ToIngredient()).ToList(),
                CreatedById = createdBy.Id,
            };
        }
    }
}
