namespace RoutingExample.Pages.ProductDetails
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly ProductService _service;
        public IndexModel(ProductService service)
        {
            this._service = service;
        }

        public Product Selected { get; set; }

        public IActionResult OnGet(string name)
        {
            this.Selected = this._service.GetProduct(name);
            if (this.Selected is null)
            {
                return this.NotFound();
            }
            return this.Page();
        }

    }
}