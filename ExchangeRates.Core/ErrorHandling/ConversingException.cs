using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.ErrorHandling
{
    public class ConversingException : Exception
    {
        public ConversingException(string msg) : base(msg)
        {
                
        }
    }
}
