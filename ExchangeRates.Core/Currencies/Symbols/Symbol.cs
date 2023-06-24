using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.Symbols
{
    internal class Symbol : ISymbol
    {

        public string CurrencySymbol { get; set; } =string.Empty;
        public string CurrencyName { get; set; } = string.Empty;

        public string GetCurrencyName()
        {
            return CurrencyName;
        }

        public string GetCurrencySymbol()
        {
            return CurrencySymbol;
        }
    }
}
