using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Core.Currencies.Symbols;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Fetchers
{
    public class FixerFetcher : IFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly string _fixedUrl;
        public FixerFetcher(string apiKey,string url)
        {
            this._httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            this._fixedUrl = url;
        }

        // this constructor is for testing only
        public FixerFetcher()
        {
            this._httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", "nQ79FQEm879L7xHxyPORbMD6PPofZMvk");
            this._fixedUrl = "https://api.apilayer.com/fixer/";
        }
        public async Task<IConverter?> ConvertTwoCurrAsync(string cur1, string cur2, decimal amount)
        {

            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + $"convert?to={cur2}&from={cur1}&amount={amount}");
                var asStr = await apiCall.Content.ReadAsStringAsync();
                var json = JObject.Parse(asStr);
                var rate = json["info"]!["rate"]!.ToObject<decimal>();
                var result = json["result"]!.ToObject<decimal>();
                if (rate <= 0m || result <= 0) return null;

                return new Converter
                {
                    FromCurrency = cur1,
                    ToCurrency = cur2,
                    Rate = rate,
                    Result = result
                };

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<ISymbol>?> FetchAllSymbolsAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "symbols");
                var asStr = await apiCall.Content.ReadAsStringAsync();
                var json = JObject.Parse(asStr);
                var sym = json["symbols"];

                if (sym is null) return null;

                var symAsDic = sym.ToObject<Dictionary<string, string>>();
                return GenerateSymbols(symAsDic);

            }
            catch (Exception)
            {

                return null;
            }
        }


        public async Task<ILatestPrice?> FetchLatestPriceAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "latest?base=USD");
                var asStr = await apiCall.Content.ReadAsStringAsync();
                var json = JObject.Parse(asStr);
                var rat = json["rates"];

                if (rat is null) return null;

                var ratAsDic = rat.ToObject<Dictionary<string, decimal>>();
                var res = new LatestPrice
                {
                    //curr date formated as year month and day
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    Rates = GenerateRates(ratAsDic)
                };

                return res;
            }
            catch (Exception)
            {

                return null;
            }

        }


        private static List<ISymbol>? GenerateSymbols(Dictionary<string, string>? symbolsAsDic)
        {
            if (symbolsAsDic is null) return null;

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

        private static List<IRate>? GenerateRates(Dictionary<string, decimal>? ratesAsDic)
        {
            if (ratesAsDic is null) return null;
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
