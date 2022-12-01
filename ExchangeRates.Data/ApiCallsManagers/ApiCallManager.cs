using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Data.ApiCallsManagers
{
    //Every api call goes through this manager register
    //This allows to manage numbers of External Api calls coz of calls limits
    public class ApiCallManager
    {
        public int Id { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
    }
}
