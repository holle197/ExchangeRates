using ExchangeRates.Core.Fetchers;
using ExchangeRates.Data.Currencies;
using ExchangeRates.Data.DataManaging;
using ExchangeRates.Web.DTOs;
using ExchangeRates.Web.Models.Currencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IFetcher _fetcher;
        private readonly IDataManager _dataManager;

        public CurrenciesController(IFetcher fetcher, IDataManager dataManager)
        {
            this._fetcher = fetcher;
            this._dataManager = dataManager;
        }


        //AFTER DEPLOYING THIS METHOD MUST BE RUNED 1 TIME TO SETUP CURRENCIES TO DB FOR FUTURE CHECKING
        //OF EXISTING SYMBOLS WHEN USER TRY TO CONVERT FROM ONE CUR TO ANOTHER
        [HttpGet("supportedCurrencies")]
        public async Task<ActionResult<CurrenciesModel>> Get()
        {
            var currencies = _dataManager.GetSupportedCurrencies();

            //Only one time can be run after deploying app
            if (currencies is null)
            {
                var allCurrencies = await _fetcher.FetchAllSymbolsAsync();
                if (allCurrencies is null)
                {
                    return new CurrenciesModel() { Success = false, ErrorMsg = "InternalServerError" };
                }
                currencies = ISymbolsToCurrencies.Convert(allCurrencies);
                await _dataManager.AddCurrencies(currencies);
                return new CurrenciesModel() { Success = true, Currencies = CurrenciesToCurrenciesModel.Convert(currencies) };
            }

            return new CurrenciesModel() { Success = true, Currencies = CurrenciesToCurrenciesModel.Convert(currencies) };
        }


    }
}
