namespace ValidatingWithDataAnnotations.Pages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public UserBindingModel Input { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            //Do something, e.g. take payment, save to database etc.

            return this.RedirectToPage("Success");
        }

        public class UserBindingModel
        {
            [Required]
            [StringLength(100)]
            [Display(Name = "Your name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
    }
}