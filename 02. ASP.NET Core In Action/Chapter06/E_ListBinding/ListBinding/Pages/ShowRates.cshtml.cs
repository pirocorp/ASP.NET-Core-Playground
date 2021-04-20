namespace ListBinding.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ShowRatesModel : PageModel
    {
        public Dictionary<string, decimal> SelectedCurrencies { get; set; }

        public void OnPost(List<string> currencies)
        {
            this.SelectedCurrencies =
                CurrencyService.AllCurrencies
                .Where(x => currencies.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}