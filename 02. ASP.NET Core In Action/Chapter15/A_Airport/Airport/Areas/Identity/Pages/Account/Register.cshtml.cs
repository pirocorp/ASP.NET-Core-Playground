namespace Airport.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Airport.Authorization;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public SelectListItem[] FrequentFlyerClasses { get; } =
            {
                new SelectListItem{Text = "Gold", Value = "Gold"},
                new SelectListItem{Text = "Silver", Value = "Silver"},
                new SelectListItem{Text = "Bronze", Value = "Bronze"},
            };

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Employee Number")]
            public string EmployeeNumber { get; set; }

            [Display(Name = "Boarding Pass Number")]
            public string BoardingPassNumber { get; set; }

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime? DateOfBirth { get; set; }

            [Display(Name = "Is banned from Lounge?")]
            public bool IsBannedFromLounge { get; set; }

            [Display(Name = "FrequentFlyerClass")]
            public string FrequentFlyerClass { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = this.Input.Email, Email = this.Input.Email };
                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    await this.AddClaims(user);
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        private async Task AddClaims(IdentityUser user)
        {
            if (this.Input.DateOfBirth.HasValue)
            {
                var newClaim = new Claim(ClaimTypes.DateOfBirth, this.Input.DateOfBirth.Value.ToString("yyyy-MM-dd"), ClaimValueTypes.Date);
                await this._userManager.AddClaimAsync(user, newClaim);
            }
            if (!string.IsNullOrEmpty(this.Input.BoardingPassNumber))
            {
                var newClaim = new Claim(Claims.BoardingPassNumber, this.Input.BoardingPassNumber);
                await this._userManager.AddClaimAsync(user, newClaim);
            }
            if (!string.IsNullOrEmpty(this.Input.EmployeeNumber))
            {
                var newClaim = new Claim(Claims.EmployeeNumber, this.Input.EmployeeNumber);
                await this._userManager.AddClaimAsync(user, newClaim);
            }
            if (!string.IsNullOrEmpty(this.Input.FrequentFlyerClass))
            {
                var newClaim = new Claim(Claims.FrequentFlyerClass, this.Input.FrequentFlyerClass);
                await this._userManager.AddClaimAsync(user, newClaim);
            }
            if (this.Input.IsBannedFromLounge)
            {
                var newClaim = new Claim(Claims.IsBannedFromLounge, "true");
                await this._userManager.AddClaimAsync(user, newClaim);
            }
        }
    }
}
