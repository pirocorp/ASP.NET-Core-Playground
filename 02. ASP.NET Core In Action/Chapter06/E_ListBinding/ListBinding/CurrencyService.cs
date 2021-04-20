namespace ListBinding
{
    using System.Collections.Generic;

    public class CurrencyService
    {
        public static readonly Dictionary<string, decimal> AllCurrencies =
            new Dictionary<string, decimal>
            {
                {"GBP", 1.00m},
                {"USD", 1.22m},
                {"CAD", 1.64m},
                {"EUR", 1.15m},
            };
    }
}
