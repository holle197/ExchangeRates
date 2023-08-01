using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Core.ErrorHandling;
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
        public static decimal ConvertBetweenTwoCurencies(decimal rate, decimal amount)
        {
            return rate * amount > 0m ? rate * amount : throw new ConversingException("Rate And Amount Must Be Great Than 0.");
        }

        //Method for conversion between 2 cur from DB with daily USD based rates
        //can be used for reverse conversion USD -> RSD   and RSD -> USD
        public static decimal GenerateMiddlePrice(List<IRate> rates, string fromCur, string toCur, decimal Amount)
        {
            //check if symbols are valid and amount is great than 0
            ValidateSymbols(rates, fromCur, toCur);
            if (Amount <= 0m) throw new ConversingException("Amount Must Be Greather Than 0.");

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

        private static void ValidateSymbols(List<IRate> rates, string sym1, string sym2)
        {
            if (sym1 == sym2) throw new ConversingException("Cannot Exchange The Same Currency. Currency1 Are The Same As Currency2.");


            else if (sym1 == "USD" && CheckSymbolExists(rates, sym2)) return;

            else if (sym2 == "USD" && CheckSymbolExists(rates, sym1)) return;
            //check if both sym1 and sym2 exists in list of IRate 
            else if (CheckSymbolExists(rates, sym1) && CheckSymbolExists(rates, sym2)) return ;
            throw new ConversingException("Symbol Does Not Exists.");
        }

        private static decimal GetRate(List<IRate> rates, string symbol)
        {
            var rate = rates.Where(rate => rate.GetSymbol() == symbol).FirstOrDefault();
            //cannot be null, all checks are in CheckSymbolExists()
             return rate!.GetRate();          
        }

        private static bool CheckSymbolExists(List<IRate> rates,string symbol)
        {
            return rates.Any(r => r.GetSymbol() == symbol);
        }


    }
}
