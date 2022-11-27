using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.RateConversion
{
    public static class OfflineRateConversion
    {
        //Method for direct conversion between 2 cur if pair is in DB
        //cannot be used for reverse conversion USD -> RSD     RSD -> USD 
        public static decimal? ConvertBetweenTwoCur(decimal rate, decimal amount)
        {
            return rate * amount > 0m ? rate * amount : null;
        }

        //Method for conversion between 2 cur from DB with daily USD based rates
        //can be used for reverse conversion USD -> RSD   and RSD -> USD
        public static decimal? GenerateMiddlePrice(List<IRate>? rates, string fromCur, string toCur, decimal Amount)
        {
            if (rates is null) return null;
            //check if symbols are valid and amount is great than 0
            if (!ValidateSymbols(rates, fromCur, toCur) || Amount <= 0) return null;

            if (fromCur == "USD")
            {
                return GetRate(rates, toCur) * Amount;
            }
            else if (toCur == "USD")
            {
                return Amount / GetRate(rates, fromCur);
            }

            var rate1 = GetRate(rates, fromCur);
            var rate2 = GetRate(rates, toCur);

            return rate2 / rate1 * Amount;
        }

        private static bool ValidateSymbols(List<IRate>? rates, string sym1, string sym2)
        {
            if (sym1 == sym2) return false;

            else if (rates is null) return false;

            else if (sym1 == "USD") return rates.Any(r => r.GetSymbol() == sym2);

            else if (sym2 == "USD") return rates.Any(r => r.GetSymbol() == sym1);
            //check if both sym1 and sym2 exists in list of IRate 
            else if (rates.Any(r => r.GetSymbol() == sym1) && rates.Any(r => r.GetSymbol() == sym2)) return true;

            return false;
        }

        private static decimal GetRate(List<IRate>? rates, string symbol)
        {
            if (rates is not null)
            {
                var rate = rates.Where(rate => rate.GetSymbol() == symbol).FirstOrDefault();
                if (rate is not null) return rate.GetRate();
            }
            return 0m;
        }


    }
}
