using ExchangeRates.Data.ApiCallsManagers;
using ExchangeRates.Data.Currencies;
using ExchangeRates.Data.DataContext;
using ExchangeRates.Data.ExchangeRates;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddDailyRates(List<DailyRate> rates)
        {
            _dataContext.DailyRates.AddRange(rates);
            AddApiCallRegister();
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ExchangeRate> AddExchangeRate(ExchangeRate exchangeRate)
        {
            _dataContext.Add(exchangeRate);
            AddApiCallRegister();
            await _dataContext.SaveChangesAsync();
            return exchangeRate;
        }

        public async Task<bool> CheckIfSymbolExist(string sym)
        {
            var res = await _dataContext.Currencies.Where(s => s.Symbol == sym).FirstOrDefaultAsync();
            return res != null;
        }

        public async Task<List<DailyRate>?> GetDailyRates()
        {
            var today = DateTime.Now.ToString("yyyy/MM/dd");
            var rates = await _dataContext.DailyRates.Where(r=>r.Date == today).ToListAsync();

            if (rates is null || rates.Count == 0) return null;
            return rates;
        }

        //for now,work only in one direction FROM -> To
        public async Task<ExchangeRate?> GetExchangeRate(string fromCur, string toCur)
        {
            var rates = await _dataContext.ExchangeRates.Where(r => r.FromCur == fromCur && r.ToCur == toCur).ToListAsync();
            var today = DateTime.Now.ToString("yyyy/MM/dd");
            if (rates is null || rates.Count == 0) return null;
            foreach (var rate in rates)
            {
                if (rate.Date == today) return rate;
            }

            return null;
        }

        public async Task<List<Currency>?> GetSupportedCurrencies()
        {
            var res = await _dataContext.Currencies.ToListAsync();
            if (res is null || res.Count <= 0) return null;
            return res;
        }

        public async Task<int> GetTotalApiCalls()
        {
            var currDate = DateTime.Now.ToString("MMMM yyyy");
            var total = await _dataContext.ApiCallManager.Where(t => t.Date == currDate).ToListAsync();
            return total?.Count ?? 0;
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
