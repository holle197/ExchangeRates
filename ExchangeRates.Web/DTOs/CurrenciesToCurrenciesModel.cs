using ExchangeRates.Data.Currencies;
using ExchangeRates.Web.Models.Currencies;

namespace ExchangeRates.Web.DTOs
{
    //convert from core Currency to CurrencyModel
    internal static class CurrenciesToCurrenciesModel
    {
        internal static List<CurrencyModel> Convert(List<Currency> currencies)
        {
            var res = new List<CurrencyModel>();
            foreach (var currency in currencies)
            {
                res.Add(CurrencyToCurrencyModel(currency));
            }
            return res;
        }

        private static CurrencyModel CurrencyToCurrencyModel(Currency cur)
        {
            return new CurrencyModel()
            {
                Name = cur.Name,
                Symbol = cur.Symbol
            };
        }
    }
}
