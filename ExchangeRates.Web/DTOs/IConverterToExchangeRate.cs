using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Data.ExchangeRates;

namespace ExchangeRates.Web.DTOs
{
    internal static class IConverterToExchangeRate
    {
        internal static ExchangeRate Convert(IConverter converter)
        {
            return new ExchangeRate()
            {
                FromCur = converter.GetFromCurr(),
                ToCur = converter.GetToCurr(),
                Rate = converter.GetRate(),
            };
        }
    }
}
