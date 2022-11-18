using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.LatestPrices
{
    internal class LatestPrice : ILatestPrice
    {
        public string BaseCurrency { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public List<IRate> Rates { get; set; } = new();
        public string GetBase()
        {
            return BaseCurrency;
        }

        public string GetDate()
        {
            return Date;
        }

        public List<IRate> GetRates()
        {
            return Rates;
        }
    }
}
