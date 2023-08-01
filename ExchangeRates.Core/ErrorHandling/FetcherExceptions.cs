using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.ErrorHandling
{
    public class FetcherExceptions : Exception
    {
        public FetcherExceptions(string msg) : base(msg)
        {
            
        }
    }
}
