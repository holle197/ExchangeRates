using ExchangeRates.Core.Fetchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExchangeRates.Test.CoreTesting.FetchersTesting
{
    public class TestingFixerFetcher
    {
        [Fact]

        public async Task FetchSymbols_OnSuccess_ReturnsISymbols()
        {
            var apikey = "nQ79FQEm879L7xHxyPORbMD6PPofZMvk";
            var fetcher = new FixerFetcher(apikey);

            var res = await fetcher.FetchAllSymbolsAsync();

            Assert.NotNull(res);
            Assert.True(res?.Count > 0);
        }


        [Fact]
        public async Task FetchLatestPrices_OnSuccess_ExpectILatestPrice()
        {
            var apikey = "nQ79FQEm879L7xHxyPORbMD6PPofZMvk";
            var fetcher = new FixerFetcher(apikey);

            var res = await fetcher.FetchLatestPrice();

            Assert.NotNull(res);
            Assert.True(res?.GetRates()?.Count > 0);
        }
    }
}
