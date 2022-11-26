using ExchangeRates.Core.Currencies.LatestPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Test.CoreTesting.RateConversionTesting.Helpers
{
    internal static class RatesHelper
    {
        public static List<IRate> GenerateRates()
        {
            return new()
            {
                new MockRate { CurrencySymbol = "EUR", CurrencyRate = 0.96m, },
                new MockRate { CurrencySymbol = "RSD", CurrencyRate = 112.64m, },
                new MockRate { CurrencySymbol = "HRK", CurrencyRate = 7.25m, },
                new MockRate { CurrencySymbol = "GBP", CurrencyRate = 0.82m, },
                new MockRate { CurrencySymbol = "CAD", CurrencyRate = 0.34m, },
                new MockRate { CurrencySymbol = "AED", CurrencyRate = 3.67m, },
                new MockRate { CurrencySymbol = "CLF", CurrencyRate = 0.033m, },
            };
        }
    }
}
