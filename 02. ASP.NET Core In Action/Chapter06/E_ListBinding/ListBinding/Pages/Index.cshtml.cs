namespace ListBinding.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using System.Collections.Generic;
    using System.Linq;

    public class IndexModel : PageModel
    {
        public List<SelectListItem> Currencies { get; set; }

        public void OnGet()
        {
            this.Currencies = CurrencyService.AllCurrencies
                .Select(x => new SelectListItem { Text = x.Key })
                .ToList();
        }
    }
}
