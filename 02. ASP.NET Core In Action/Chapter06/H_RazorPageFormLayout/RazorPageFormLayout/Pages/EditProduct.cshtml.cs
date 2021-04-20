namespace RazorPageFormLayout.Pages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class EditProductModel : PageModel
    {
        private readonly ProductService _productService;

        public EditProductModel(ProductService productService)
        {
            this._productService = productService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        
        public IActionResult OnGet(int id)
        {
            var product = this._productService.GetProduct(id);
            if(product is null)
            {
                return this.NotFound();
            }

            this.Input = new InputModel
            {
                Name = product.ProductName,
                Price = product.SellPrice,
            };

            return this.Page();
        }

        public IActionResult OnPost(int id)
        {
            var product = this._productService.GetProduct(id);
            if (product is null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._productService.UpdateProduct(id, this.Input.Name, this.Input.Price);
            return this.RedirectToPage("Index");
        }


        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0, int.MaxValue)]
            public decimal Price { get; set; }
        }
    }
}