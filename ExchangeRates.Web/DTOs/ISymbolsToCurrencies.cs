using ExchangeRates.Core.Currencies.Symbols;
using ExchangeRates.Data.Currencies;

namespace ExchangeRates.Web.DTOs
{
    //convert from core ISymbol to db class Currency
    internal static class ISymbolsToCurrencies
    {
        internal static List<Currency> Convert(List<ISymbol> symbols)
        {
            var res = new List<Currency>();
            foreach (var isym in symbols)
            {
                res.Add
                    (
                        ISymToCur(isym)
                    ); ;
            }
            return res;
        }

        private static Currency ISymToCur(ISymbol sym)
        {
            return new Currency()
            {
                Name = sym.GetCurrencyName(),
                Symbol = sym.GetCurrencySymbol(),
            };
        }

    }
}
