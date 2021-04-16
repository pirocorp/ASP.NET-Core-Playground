namespace RoutingExample.Pages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Routing;

    public class SearchModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly LinkGenerator _link;

        public SearchModel(ProductService productService, LinkGenerator link)
        {
            this._productService = productService;
            this._link = link;
        }

        [BindProperty, Required]
        public string SearchTerm { get; set; }

        public List<Product> Results { get; private set; }

        public void OnGet()
        {
            // Demonstrates link generation 
            var url1 = this.Url.Page("ProductDetails/Index", new { name = "big-widget" });
            var url2= this._link.GetPathByPage("/ProductDetails/Index", values: new { name = "big-widget" });
            var url3 = this._link.GetPathByPage(this.HttpContext, "/ProductDetails/Index", values: new { name = "big-widget" });
            var url4 = this._link.GetUriByPage(
                page: "/ProductDetails/Index",
                handler: null,
                values: new { name = "big-widget" },
                scheme: "https",
                host: new HostString("example.com"));
        }

        public void OnPost()
        {
            if (this.ModelState.IsValid)
            {
                this.Results = this._productService.Search(this.SearchTerm, StringComparison.Ordinal);
            }

        }
        public void OnPostIgnoreCase()
        {
            if (this.ModelState.IsValid)
            {
                this.Results = this._productService.Search(this.SearchTerm, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}