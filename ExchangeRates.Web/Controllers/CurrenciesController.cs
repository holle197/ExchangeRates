using ExchangeRates.Core.Fetchers;
using ExchangeRates.Data.DataManaging;
using ExchangeRates.Web.Models.Currencies;
using ExchangeRates.Web.Models.Rates;
using ExchangeRates.Web.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IFetcher _fetcher;
        private readonly IDataManager _dataManager;
        private readonly SupportedCurrenciesServices _supportedCurrenciesServices;
        private readonly ConvertServices _convertServices;

        public CurrenciesController(IFetcher fetcher, IDataManager dataManager)
        {
            this._fetcher = fetcher;
            this._dataManager = dataManager;
            this._supportedCurrenciesServices = new(fetcher,dataManager);
            this._convertServices = new(fetcher,dataManager);
        }

        //AFTER DEPLOYING THIS METHOD MUST BE RUNED 1 TIME TO SETUP CURRENCIES TO DB FOR FUTURE CHECKING
        //OF EXISTING SYMBOLS WHEN USER TRY TO CONVERT FROM ONE CUR TO ANOTHER
        [HttpGet("supportedCurrencies")]
        public async Task<ActionResult<CurrenciesModel>> Get()
        {
            return await _supportedCurrenciesServices.GetSupportedCurrencies();       
        }


        [HttpGet("convert")]
        public async Task<ActionResult<ExchangeRateModel>> Convert(string from, string to, decimal amount)
        {                    
            return await _convertServices.Convert(from,to,amount);
        }
        
    }
}
