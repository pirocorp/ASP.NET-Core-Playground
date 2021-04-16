namespace PageHandlers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SearchModel : PageModel
    {
        private readonly SearchService _searchService;

        public SearchModel(SearchService searchService)
        {
            this._searchService = searchService;
        }

        [BindProperty]
        public BindingModel Input { get; set; }

        public List<Product> Results { get; set; }

        // Input property decorated with [BindProperty] is not bound for HTTP GET requests.
        // To bind properties for GET requests use [BindProperty(SupportsGet = true)]
        public IActionResult OnGet()
        {
            return this.Page();
        }

        // Unlike most .NET classes, you can’t use method overloading to have multiple page handlers
        // on a Razor Page with the same name.
        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToPage();
            }

            this.Results = this._searchService.SearchProducts(this.Input.SearchTerm);
            return this.Page();
        }

        public class BindingModel
        {
            [Required]
            public string SearchTerm { get; set; }
        }
    }
}