namespace CrossSiteScripting.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ProtectedModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        public void OnGet()
        {
            this.Name = DataService.GetMaliciousValue();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            DataService.Data.Add(this.Name);
            return this.RedirectToPage("/protected");
        }
    }
}
