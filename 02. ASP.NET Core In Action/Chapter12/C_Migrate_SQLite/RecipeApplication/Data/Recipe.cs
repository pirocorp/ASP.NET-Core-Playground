﻿namespace RecipeApplication.Data
{
    using System;
    using System.Collections.Generic;

    public class Recipe
    {
        public int RecipeId { get; set; }

        public string Name { get; set; }

        public TimeSpan TimeToCook { get; set; }

        public bool IsDeleted { get; set; }

        public string Method { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsVegan { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
