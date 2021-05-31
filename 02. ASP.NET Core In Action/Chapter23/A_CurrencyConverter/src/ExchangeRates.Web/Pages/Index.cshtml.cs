namespace ExchangeRates.Web.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Options;

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class IndexModel : PageModel
    {
        private readonly CurrencySettings _settings;
        private readonly ICurrencyConverter _converter;

        public IndexModel(IOptions<CurrencySettings> settings, ICurrencyConverter converter)
        {
            this._settings = settings.Value;
            this._converter = converter;
        }

        [DisplayName("Value in alternate currency")]
        public decimal Result { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
            this.Input = new InputModel()
            {
                Value = this._settings.DefaultValue,
                ExchangeRate = this._settings.DefaultExchangeRate,
                DecimalPlaces = this._settings.DefaultDecimalPlaces,
            };
        }

        public PageResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.Result = this._converter.ConvertToGbp(this.Input.Value, this.Input.ExchangeRate, this.Input.DecimalPlaces);

            return this.Page();
        }

        public class InputModel
        {
            [DisplayName("Value in GBP")]
            public decimal Value { get; set; } = 0;

            [DisplayName("Exchange rate from GBP to alternate currency")]
            [Range(0, double.MaxValue)]
            public decimal ExchangeRate { get; set; }

            [DisplayName("Round to decimal places")]
            [Range(0, int.MaxValue)]
            public int DecimalPlaces { get; set; }
        }
    }
}
