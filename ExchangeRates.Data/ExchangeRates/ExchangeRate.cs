using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Data.ExchangeRates
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string FromCur { get; set; } = string.Empty;
        public string ToCur { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
    }
}
