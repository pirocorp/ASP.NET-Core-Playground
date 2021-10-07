namespace ShoppingCart.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;

            this.Products = new List<Product>();
        }

        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
            this.Products = this._db.Products
                .ToList()
                .OrderBy(p => int.Parse(p.Title.Split(" ")[1]));
        }
    }
}
