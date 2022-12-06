using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Data.ExchangeRates;

namespace ExchangeRates.Web.DTOs
{
    internal static class IRatesToDailyRate
    {
        internal static List<DailyRate> Convert(List<IRate> rates)
        {
            var res = new List<DailyRate>();
            foreach (var rate in rates)
            {
                res.Add
                    (
                    new DailyRate()
                    {
                        Symbol = rate.GetSymbol(),
                        Rate = rate.GetRate(),
                        Date = DateTime.Now.ToString("yyyy/MM/dd")

                    }
                    );
            }
            return res;
        }
    }
}
