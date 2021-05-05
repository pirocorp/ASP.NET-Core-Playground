using System.Collections.Generic;

namespace RecipeApplication.Models
{
    public class RecipeDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Method { get; set; }

        public IEnumerable<Item> Ingredients { get; set; }

        public class Item
        {
            public string Name { get; set; }

            public string Quantity { get; set; }
        }
    }
}
