namespace ExampleBinding.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [IgnoreAntiforgeryToken] // So you can call the page from Postman etc
    public class EditProductModel : PageModel
    {
        public ProductModel Product { get; set; }

        public void OnGet()
        {

        }

        public void OnPost(ProductModel product)
        {
            this.Product = product;
        }

        public void OnPostEditTwoProducts(ProductModel product1, ProductModel product2)
        {
            this.Product = product1;
        }
    }
}