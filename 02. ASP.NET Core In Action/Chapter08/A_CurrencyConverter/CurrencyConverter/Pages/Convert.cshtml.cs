namespace CurrencyConverter.Pages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ConvertModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public SelectListItem[] CurrencyCodes { get; } =
        {
            new() {Text="GBP", Value = "GBP"},
            new() {Text="USD", Value = "USD"},
            new() {Text="CAD", Value = "CAD"},
            new() {Text="EUR", Value = "EUR"},
        };

        public void OnGet()
        {
            this.Input = new InputModel();
        }

        public IActionResult OnPost()
        {
            if (this.Input.CurrencyFrom == this.Input.CurrencyTo)
            {
                this.ModelState.AddModelError(string.Empty, "Cannot convert currency to itself");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            // Store the valid values somewhere (e.g. database), 
            // do the conversion etc

            return this.RedirectToPage("Success");
        }

        public class InputModel
        {
            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode("GBP", "USD", "CAD", "EUR")]
            [Display(Name = "Currency From")]
            public string CurrencyFrom { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode("GBP", "USD", "CAD", "EUR")]
            [Display(Name = "Currency To")]
            public string CurrencyTo { get; set; }

            [Required]
            [Range(1, 1000)]
            public int Quantity { get; set; }
        }
    }
}