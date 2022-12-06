using ExchangeRates.Core.Currencies.LatestPrices;

namespace ExchangeRates.Web.Models.Rates
{
    public class DbRate : IRate
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal GetRate()
        {
            return Rate;
        }

        public string GetSymbol()
        {
            return Symbol;
        }
    }
}
