namespace RoutingExample.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ProductsModel : PageModel
    {
        private readonly ProductService _service;

        public ProductsModel(ProductService service)
        {
            this._service = service;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        public Product Selected { get; set; }

        public IActionResult OnGet()
        {
            this.Selected = this._service.GetProduct(this.Name);
            if(this.Selected is null)
            {
                return this.NotFound();
            }
            return this.Page();
        }

    }
}