using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Currencies.LatestPrices
{
    public interface ILatestPrice
    {
       
        string GetBase();
        string GetDate();

        /// <summary>
        /// List of all currencies with rates based on BASE currency
        /// </summary>
        /// <returns></returns>
        List<IRate> GetRates();
    }
}
