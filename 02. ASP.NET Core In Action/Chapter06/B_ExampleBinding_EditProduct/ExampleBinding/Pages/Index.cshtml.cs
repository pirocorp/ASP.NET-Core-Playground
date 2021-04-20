namespace ExampleBinding.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [IgnoreAntiforgeryToken] // So you can call the page from 
    public class IndexModel : PageModel
    {
        public ProductModel Product { get; set; }

        public RedirectToPageResult OnGet()
        {
            return this.RedirectToPage("EditProduct");
        }

        public void OnPostEditProduct(ProductModel product)
        {
            this.Product = product;
        }
        public void OnPostEditTwoProducts(ProductModel product1, ProductModel product2)
        {
            this.Product = product1;
        }
    }
}
