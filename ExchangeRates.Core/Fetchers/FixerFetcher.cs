using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.Currencies.LatestPrices;
using ExchangeRates.Core.Currencies.Symbols;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public async Task<ILatestPrice> FetchLatestPrice()
        {
            throw new NotImplementedException();
        }




        private static List<ISymbol>? GenerateSymbols(Dictionary<string, string>? symbolsAsDic)
        {
            if (symbolsAsDic == null) return null;

            var res = new List<ISymbol>();
            foreach (var i in symbolsAsDic)
            {
                var symbol = new Symbol
                {
                    CurrencySymbol = i.Key,
                    CurrencyName = i.Value
                };
                res.Add(symbol);
            }
            return res;
        }
    }
}
