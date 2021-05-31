namespace Airport.Pages.Airport
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize("CanAccessLounge")]
    public class AirportLoungeModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}