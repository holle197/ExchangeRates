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
        public async Task FetchLatestPrices_OnSuccess_ReturnILatestPrice()
        {
            var apikey = "nQ79FQEm879L7xHxyPORbMD6PPofZMvk";
            var fetcher = new FixerFetcher(apikey);

            var res = await fetcher.FetchLatestPriceAsync();

            Assert.NotNull(res);
            Assert.True(res?.GetRates()?.Count > 0);
        }

        [Fact]
        public async Task ConvertTwoCurr_OnSuccess_ReturnsIConvert()
        {
            var apikey = "nQ79FQEm879L7xHxyPORbMD6PPofZMvk";
            var fetcher = new FixerFetcher(apikey);

            var res = await fetcher.ConvertTwoCurrAsync("USD","EUR",2.4m);

            Assert.NotNull(res);
            Assert.True(res?.GetRate() > 0m);
            Assert.True(res?.GetResult() > 0m);

        }
    }
}
