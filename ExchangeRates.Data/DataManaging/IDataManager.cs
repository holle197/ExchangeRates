using ExchangeRates.Data.ApiCallsManagers;
using ExchangeRates.Data.Currencies;
using ExchangeRates.Data.ExchangeRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Data.DataManaging
{
    public interface IDataManager
    {
        Task<List<Currency>> AddCurrencies(List<Currency> currencies);
        List<Currency>? GetSupportedCurrencies();
        Task<bool> CheckIfSymbolExist(string sym);

        Task<ExchangeRate> AddExchangeRate(ExchangeRate exchangeRate);
        Task<ExchangeRate?> GetExchangeRate(string fromCur,string toCur);

        Task<bool> AddDailyRates();
        Task<DailyRate?> GetDailyRate(string cur1,string cur2);

        int GetTotalApiCalls();

    }
}
