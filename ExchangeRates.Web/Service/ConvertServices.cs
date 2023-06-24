using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Fetchers;
using ExchangeRates.Core.RateConversion;
using ExchangeRates.Data.DataManaging;
using ExchangeRates.Data.ExchangeRates;
using ExchangeRates.Web.DTOs;
using ExchangeRates.Web.Models.Rates;

namespace ExchangeRates.Web.Service
{
    public class ConvertServices
    {
        private readonly IFetcher _fetcher;
        private readonly IDataManager _dataManager;

        public ConvertServices(IFetcher fetcher,IDataManager dataManager)
        {
            this._fetcher = fetcher;
            this._dataManager = dataManager;
        }
        public async Task<ExchangeRateModel> Convert(string from,string to,decimal amount)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            bool chechValidationOfInpParams = await IsInputParametersValid(from, to, amount);
            if (chechValidationOfInpParams == false)
            {
                return new ExchangeRateModel() { Success = false, ErrorMessage = "InvalidParameters" };
            }

            // db have current exchange rate for given pair and time of fetched data is not older than 1 day
            var rateFromDb = await _dataManager.GetExchangeRate(from, to);
            if (rateFromDb is not null)
            {
                return ConvertFromDb(rateFromDb, amount);
            }


            //there are enough free api calls so user can get direct exchange rate
            if (await HaveEnoughFreeApiCalls())
            {
                var rate = await _fetcher.Convert(from, to, amount);
                //if (rate is null) return new ExchangeRateModel() { ErrorMsg = "InternalServerError" };
                return await ConvertFromFetcher(rate, amount);
            }

            //there are not enough free api calls
            var dailyRates = await _dataManager.GetDailyRates();
            if (dailyRates is null)
            {
                var latestPrices = await _fetcher.FetchLatestPriceAsync();
                var rates = latestPrices?.GetRates();
                //if (rates is null) return new ExchangeRateModel() { ErrorMsg = "InternalServerError" };
                var res = IRatesToDailyRate.Convert(rates);
                await _dataManager.AddDailyRates(res);
                return ConvertFromDbDailyRates(res, from, to, amount);

            }

            return ConvertFromDbDailyRates(dailyRates, from, to, amount);
        }

        private async Task<bool> IsInputParametersValid(string from, string to, decimal amount)
        {
            bool validateFromCur = await _dataManager.CheckIfSymbolExist(from);
            bool validateToCur = await _dataManager.CheckIfSymbolExist(to);
            bool validateAmount = amount > 0m;
            bool fromIsDifThanTo = from != to;

            if (validateFromCur && validateToCur && validateAmount && fromIsDifThanTo) return true;
            return false;
        }

        private ExchangeRateModel ConvertFromDb(ExchangeRate exchangeRate, decimal amount)
        {
            var total = OfflineRateConversion.ConvertBetweenTwoCurencies(exchangeRate.Rate, amount);

            var result = ExchangeRateToExchangeRateModel.Convert(exchangeRate);
            result.Success = true;
            //cannot be null,all checks passed
            result.Result = total ?? 0;
            result.Amount = amount;
            result.LatesUpdate = DateTime.Now.ToString("yyyy/MM/dd");

            return result;
        }


        //check if there are enough free api calls
        private async Task<bool> HaveEnoughFreeApiCalls()
        {
            var maxApiCallsFromSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ApiCallsSettings")["MaxApiCallsPerMonth"];
            var maxApiCallsPerMonth = Int32.Parse(maxApiCallsFromSettings!);
            var totalApiCallsThisMonth = await _dataManager.GetTotalApiCalls();

            var curYear = (int)DateTime.Now.Year;
            var curMonth = (int)DateTime.Now.Month;
            int maxDaysInCurMonth = DateTime.DaysInMonth(curYear, curMonth);
            int currDay = DateTime.Now.Day;

            var freeApiCalls = maxApiCallsPerMonth - totalApiCallsThisMonth;
            var reservedCalls = maxDaysInCurMonth - currDay;

            return freeApiCalls > reservedCalls;
        }



        //fetch rates from given fetcher and write to db result and return ExchangeRateModel
        private async Task<ExchangeRateModel> ConvertFromFetcher(IConverter converter, decimal amount)
        {
            var IConverterAsRate = IConverterToExchangeRate.Convert(converter);
            await _dataManager.AddExchangeRate(IConverterAsRate);
            var res = ExchangeRateToExchangeRateModel.Convert(IConverterAsRate);
            res.Amount = amount;
            res.Success = true;
            res.Result = converter.GetResult();
            //full datetime 
            res.LatesUpdate = DateTime.Now.ToString();
            return res;
        }


        private ExchangeRateModel ConvertFromDbDailyRates(List<DailyRate> dailyRates, string from, string to, decimal amount)
        {
            var dailyRateToIRate = RateToIRate.Convert(dailyRates);
            var result = OfflineRateConversion.GenerateMiddlePrice(dailyRateToIRate, from, to, amount);
            return new ExchangeRateModel()
            {
                Success = true,
                FromCurrency = from,
                ToCurrency = to,
                Amount = amount,
                Rate = result / amount ?? 0,
                Result = result ?? 0,
                LatesUpdate = DateTime.Now.ToString("yyyy/MM/dd")
            };
        }
    }
}
