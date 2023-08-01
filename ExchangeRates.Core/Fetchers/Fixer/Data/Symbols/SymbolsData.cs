using ExchangeRates.Core.Currencies.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Fetchers.Fixer.Data.Symbols
{
    internal class SymbolsData : ISymbol
    {
        public bool success { get; set; }
        public Dictionary<string,string> symbols { get; set; } = new();

        public string GetCurrencyName()
        {
            throw new NotImplementedException();
        }

        public string GetCurrencySymbol()
        {
            throw new NotImplementedException();
        }
    }
}
