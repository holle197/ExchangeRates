using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Data.ExchangeRates;
using ExchangeRates.Web.Models.Rates;

namespace ExchangeRates.Web.DTOs
{
    internal static class RateToIRate
    {
        internal static List<IRate> Convert(List<DailyRate> dailyRates)
        {
            var res = new List<IRate>();
            foreach (var rate in dailyRates)
            {
                res.Add
                    (
                        new DbRate() { Symbol = rate.Symbol, Rate = rate.Rate }
                    );
            }
            return res;
        }
    }
}
