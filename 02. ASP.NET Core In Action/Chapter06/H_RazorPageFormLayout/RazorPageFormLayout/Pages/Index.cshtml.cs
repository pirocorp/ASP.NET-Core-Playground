namespace RazorPageFormLayout.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System.Collections.Generic;

    public class IndexModel : PageModel
    {

        private readonly ProductService _productService;
        public IndexModel(ProductService productService)
        {
            this._productService = productService;
        }

        public Dictionary<int, ProductDetails> Products { get; private set; }

        public void OnGet()
        {
            this.Products = this._productService._products;
        }
    }
}
