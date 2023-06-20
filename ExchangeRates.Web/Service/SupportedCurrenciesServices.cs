using ExchangeRates.Core.Fetchers;
using ExchangeRates.Data.DataManaging;
using ExchangeRates.Web.DTOs;
using ExchangeRates.Web.Models.Currencies;

namespace ExchangeRates.Web.Service
{
    public class SupportedCurrenciesServices
    {
        private readonly IFetcher _fetcher;
        private readonly IDataManager _dataManager;

        public SupportedCurrenciesServices(IFetcher fetcher, IDataManager dataManager)
        {
            this._fetcher = fetcher;
            this._dataManager = dataManager;
        }

        public async Task<CurrenciesModel> GetSupportedCurrencies()
        {
            var currencies = await _dataManager.GetSupportedCurrencies();
        
            if (currencies is null)
            {
                return await GetSupportedCurrenciesFromFetcherAdnStoreInDb();
            }

            return new CurrenciesModel() { Success = true, Currencies = CurrenciesToCurrenciesModel.Convert(currencies) };
        }


        //fetching data and store in db
        private async Task<CurrenciesModel> GetSupportedCurrenciesFromFetcherAdnStoreInDb()
        {
            var allCurrencies = await _fetcher.FetchAllSymbolsAsync();
            if (allCurrencies is null)
            {
                return new CurrenciesModel() { Success = false, ErrorMsg = "InternalServerError" };
            }
            var currencies = ISymbolsToCurrencies.Convert(allCurrencies);
            await _dataManager.AddCurrencies(currencies);
            return new CurrenciesModel() { Success = true, Currencies = CurrenciesToCurrenciesModel.Convert(currencies) };
        }

     
    }
}
