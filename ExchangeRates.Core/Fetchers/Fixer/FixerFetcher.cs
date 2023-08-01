using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Core.Currencies.Symbols;
using ExchangeRates.Core.ErrorHandling;
using ExchangeRates.Core.Fetchers.Fixer.Data.Converting;
using ExchangeRates.Core.Fetchers.Fixer.Data.LatestPrice;
using ExchangeRates.Core.Fetchers.Fixer.Data.Symbols;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Fetchers.Fixer
{
    public class FixerFetcher : IFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly string _fixedUrl;
        public FixerFetcher(string apiKey, string url)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            _fixedUrl = url;
        }

        // this constructor is for testing only
        public FixerFetcher()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", "nQ79FQEm879L7xHxyPORbMD6PPofZMvk");
            _fixedUrl = "https://api.apilayer.com/fixer/";
        }
        public async Task<IConverter> ConvertAsync(string fromCurr, string toCurr, decimal amount)
        {
            if (fromCurr == toCurr) throw new FetcherExceptions("Cannot Convert The Same Currency.");
            if (amount <= 0m) throw new FetcherExceptions("Amount Must Be Greather Than 0.");
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + $"convert?to={toCurr}&from={fromCurr}&amount={amount}");
                var res = await apiCall.Content.ReadFromJsonAsync<ConvertingData>();

                if (res is { info: not null } && res.info.rate > 0m && res.result > 0m)
                {
                    return new Converter
                    {
                        FromCurrency = fromCurr,
                        ToCurrency = toCurr,
                        Rate = res.info.rate,
                        Result = res.result
                    };
                }

                throw new FetcherExceptions("Internal Server Error");

            }
            catch (Exception e)
            {

                throw new FetcherExceptions(e.Message);
            }
        }

        public async Task<List<ISymbol>> FetchAllSymbolsAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "symbols");
                var res = await apiCall.Content.ReadFromJsonAsync<SymbolsData>();

                if (res is { symbols: not null }) return GenerateSymbols(res.symbols);
                
                throw new FetcherExceptions("Internal Server Error");

            }
            catch (Exception e)
            {

                throw new FetcherExceptions(e.Message);
            }
        }


        public async Task<ILatestPrice> FetchLatestPriceAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "latest?base=USD");
                var res = await apiCall.Content.ReadFromJsonAsync<LatestPriceData>() ?? throw new NullReferenceException();

                if (res is null || res.rates.Count < 1) throw new FetcherExceptions("Internal Server Error");

                var latestPrices = new LatestPrice
                {
                    //curr date formated as year month and day
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    Rates = GenerateRates(res.rates)
                };

                return latestPrices;
            }
            catch (Exception)
            {

                throw;
            }

        }


        private static List<ISymbol> GenerateSymbols(Dictionary<string, string> symbolsAsDic)
        {
            var res = new List<ISymbol>();
            foreach (var i in symbolsAsDic)
            {
                res.Add(new Symbol
                {
                    CurrencySymbol = i.Key,
                    CurrencyName = i.Value
                });
            }
            return res;
        }

        private static List<IRate> GenerateRates(Dictionary<string, decimal> ratesAsDic)
        {
            var res = new List<IRate>();
            foreach (var i in ratesAsDic)
            {
                res.Add(new Rate
                {
                    CurrencySymbol = i.Key,
                    CurrencyRate = i.Value
                });
            }
            return res;
        }


    }
}
