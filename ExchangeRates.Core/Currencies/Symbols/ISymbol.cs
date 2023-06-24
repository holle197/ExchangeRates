using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.Symbols
{
    public interface ISymbol
    {

        /// <summary>
        /// Get Currency symbol like USD , EUR etc...
        /// </summary>
        /// <returns>string</returns>
        string GetCurrencySymbol();

        /// <summary>
        /// Get Currency full name like United States Dollar
        /// </summary>
        /// <returns>string</returns>
        string GetCurrencyName();
    }
}
