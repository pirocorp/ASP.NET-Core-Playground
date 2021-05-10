namespace BackgroundServiceCache
{
    using System.Collections.Generic;

    public class ExchangeRates
    {
        public string Base { get; set; }

        public string Date { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}
