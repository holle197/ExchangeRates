using ExchangeRates.Data.ApiCallsManagers;
using ExchangeRates.Data.Currencies;
using ExchangeRates.Data.DataContext;
using ExchangeRates.Data.ExchangeRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Data.DataManaging
{
    public class DataManager : IDataManager
    {
        private readonly DbDataContext _dataContext;
        public DataManager(DbDataContext dataUserContext)
        {
            this._dataContext = dataUserContext;
        }
        public async Task<List<Currency>> AddCurrencies(List<Currency> currencies)
        {
            _dataContext.Currencies.AddRange(currencies);
            AddApiCallRegister();
            await _dataContext.SaveChangesAsync();
            return currencies;
        }

        public Task<bool> AddDailyRates()
        {
            throw new NotImplementedException();
        }

        public Task<ExchangeRate> AddExchangeRate(ExchangeRate exchangeRate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfSymbolExist(string sym)
        {
            throw new NotImplementedException();
        }

        public Task<DailyRate?> GetDailyRate(string cur1, string cur2)
        {
            throw new NotImplementedException();
        }

        public Task<ExchangeRate?> GetExchangeRate(string fromCur, string toCur)
        {
            throw new NotImplementedException();
        }

        public List<Currency>? GetSupportedCurrencies()
        {
            var res = _dataContext.Currencies.ToList();
            if (res is null || res.Count <= 0) return null;
            return res;
        }

        public int GetTotalApiCalls()
        {
            var currDate = DateTime.Now.ToString("MMMM yyyy");
            var total = _dataContext.ApiCallManager.Where(t => t.Date == currDate).ToList();
            return total?.Count() ?? 0;
        }


        private void AddApiCallRegister()
        {
            var apiRegister = new ApiCallManager()
            {
                Date = DateTime.Now.ToString("MMMM yyyy")
            };
            _dataContext.ApiCallManager.Add(apiRegister);
        }
    }
}
