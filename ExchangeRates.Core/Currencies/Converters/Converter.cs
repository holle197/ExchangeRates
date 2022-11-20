using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.Converters
{
    internal class Converter : IConverter
    {
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
        public string GetFromCurr()
        {
            return FromCurrency;
        }

        public decimal GetRate()
        {
            return Rate;
        }

        public string GetToCurr()
        {
            return ToCurrency;
        }

        public decimal GetResult()
        {
            return Result;
        }
    }
}
