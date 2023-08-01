using ExchangeRates.Core.Fetchers;
using ExchangeRates.Core.Fetchers.Fixer;
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
            var fetcher = new FixerFetcher();

            var res = await fetcher.FetchAllSymbolsAsync();

            Assert.NotNull(res);
            Assert.True(res?.Count > 0);
        }


        [Fact]
        public async Task FetchLatestPrices_OnSuccess_ReturnILatestPrice()
        {
            var fetcher = new FixerFetcher();

            var res = await fetcher.FetchLatestPriceAsync();

            Assert.NotNull(res);
            Assert.True(res?.GetRates()?.Count > 0);
        }

        [Fact]
        public async Task ConvertTwoCurr_OnSuccess_ReturnsIConvert()
        {
            var fetcher = new FixerFetcher();

            var res = await fetcher.ConvertAsync("USD", "EUR", 2.4m);

            Assert.NotNull(res);
            Assert.True(res?.GetRate() > 0m);
            Assert.True(res?.GetResult() > 0m);

        }

    }
}
