namespace TagHelpers.Pages
{
    using System;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class DemoModel : PageModel
    {
        public int Int { get; set; }

        public bool Boolean { get; set; }

        public DateTime? DateTime { get; set; }

        public void OnGet()
        {

        }
    }
}