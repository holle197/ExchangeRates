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
        private const string _fixedUrl = "https://api.apilayer.com/fixer/";

        public FixerFetcher(string apiKey)
        {
            this._httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
        }
        public async Task<IConverter> ConvertTwoCurr(string cur1, string cur2)
        {
            throw new NotImplementedException();
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


        public async Task<ILatestPrice?> FetchLatestPrice()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "latest?base=USD");
                var asStr = await apiCall.Content.ReadAsStringAsync();
                var json = JObject.Parse(asStr);
                var rat = json["rates"];

                if (rat is null) return null;

                var ratAsDic = rat.ToObject<Dictionary<string, decimal>>();
                var res = new LatestPrice();
                //curr date formated as year month and day
                res.Date = DateTime.Now.ToString("yyyy/MM/dd");
                res.Rates = GenerateRates(ratAsDic);

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
