using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.LatestPrices
{
    public interface IRate
    {
        string GetSymbol();
        decimal GetRate();
    }
}
