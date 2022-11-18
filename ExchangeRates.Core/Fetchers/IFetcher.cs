using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Core.Currencies.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Fetchers
{
    public interface IFetcher
    {
        Task<List<ISymbol>?> FetchAllSymbolsAsync();
        Task<IConverter> ConvertTwoCurr(string cur1,string cur2);
        /// <summary>
        /// Fetch all prices(rates) based on USD
        /// </summary>
        /// <returns>ILatestPrice</returns>
        Task<ILatestPrice> FetchLatestPrice();
    }
}
