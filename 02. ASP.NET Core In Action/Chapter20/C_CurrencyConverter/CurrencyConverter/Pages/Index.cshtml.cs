namespace CurrencyConverter.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System.ComponentModel.DataAnnotations;

    public class IndexModel : PageModel
    {
        public IndexModel(ICurrencyProvider provider)
        {
            this.Currencies = provider.GetCurrencies();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string[] Currencies { get; set; }

        public string Results { get; set; }

        public void OnGet()
        {
            this.Input = new InputModel
            {
                CurrencyFrom = "CAD",
                CurrencyTo = "USD",
                Quantity = 50
            };
        }

        public void OnPost()
        {
            this.Results = this.ModelState.IsValid
                ? $"Converting {this.Input.Quantity} {this.Input.CurrencyFrom} to {this.Input.CurrencyTo}"
                : "Please correct the errors";
        }


        public class InputModel
        {
            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode("GBP", "USD", "CAD", "EUR")]
            public string CurrencyFrom { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCodeWithDi]
            public string CurrencyTo { get; set; }

            [Required]
            [Range(1, 1000)]
            public decimal Quantity { get; set; }
        }
    }
}
