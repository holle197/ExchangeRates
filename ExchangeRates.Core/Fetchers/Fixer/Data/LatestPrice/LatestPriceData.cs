using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Fetchers.Fixer.Data.LatestPrice
{
    internal class LatestPriceData
    {
        public Dictionary<string, decimal> rates { get; set; } = new();
    }
}
