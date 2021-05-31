namespace Airport.Pages.Airport
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize("CanEnterSecurity")]
    public class AirportSecurityModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}