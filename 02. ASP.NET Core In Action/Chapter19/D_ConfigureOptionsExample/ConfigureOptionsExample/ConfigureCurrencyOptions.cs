namespace ConfigureOptionsExample
{
    using Microsoft.Extensions.Options;

    public class ConfigureCurrencyOptions : IConfigureOptions<CurrencyOptions>
    {
        private readonly ICurrencyProvider _currencyProvider;

        public ConfigureCurrencyOptions(ICurrencyProvider currencyProvider)
        {
            this._currencyProvider = currencyProvider;
        }

        // This method is called whe an instance of IOptions<CurrencyOptions> is required.
        public void Configure(CurrencyOptions options)
        {
            options.Currencies = this._currencyProvider.GetCurrencies();
        }
    }
}
