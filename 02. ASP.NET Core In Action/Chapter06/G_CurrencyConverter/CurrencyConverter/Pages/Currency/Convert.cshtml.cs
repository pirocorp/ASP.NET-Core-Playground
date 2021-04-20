﻿namespace CurrencyConverter.Pages.Currency
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ConvertModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            return this.RedirectToPage("Success");
        }


        public class InputModel
        {
            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode("GBP", "USD", "CAD", "EUR")]
            public string CurrencyFrom { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode("GBP", "USD", "CAD", "EUR")]
            public string CurrencyTo { get; set; }

            [Required]
            [Range(1, 1000)]
            public decimal Quantity { get; set; }
        }
    }
}