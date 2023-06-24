using ExchangeRates.Data.ExchangeRates;
using ExchangeRates.Web.Models.Rates;

namespace ExchangeRates.Web.DTOs
{
    internal static class ExchangeRateToExchangeRateModel
    {
        internal static ExchangeRateModel Convert(ExchangeRate exchangeRate)
        {
            return new ExchangeRateModel()
            {
                FromCurrency = exchangeRate.FromCur,
                ToCurrency = exchangeRate.ToCur,
                Rate = exchangeRate.Rate,
            };
        }
    }
}
