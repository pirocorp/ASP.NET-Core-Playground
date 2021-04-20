namespace ManageUsers.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ViewUserModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        public void OnGet()
        {
        }

    }
}
