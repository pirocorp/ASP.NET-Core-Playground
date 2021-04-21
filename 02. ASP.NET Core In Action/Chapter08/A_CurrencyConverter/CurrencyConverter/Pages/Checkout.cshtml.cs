namespace CurrencyConverter.Pages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CheckoutModel : PageModel
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
            [StringLength(100, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Your name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone(ErrorMessage = "Not a valid phone number.")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
    }
}