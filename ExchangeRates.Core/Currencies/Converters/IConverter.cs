using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.Converters
{
    public interface IConverter
    {
        string GetFromCurr();
        string GetToCurr();
        decimal GetRate();

    }
}
