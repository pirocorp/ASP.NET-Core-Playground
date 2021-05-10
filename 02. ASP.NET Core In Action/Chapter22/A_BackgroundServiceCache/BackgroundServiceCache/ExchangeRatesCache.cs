namespace BackgroundServiceCache
{
    using System.Threading;

    public class ExchangeRatesCache
    {
        private ExchangeRates _rates;

        public ExchangeRates GetLatestRates()
        {
            // Could be null the first time it's requested
            return this._rates;
        }

        public void SetRates(ExchangeRates newRates)
        {
            Interlocked.Exchange(ref this._rates, newRates);
        }
    }
}
