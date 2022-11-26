using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRates.Core.Currencies.Converters;
using ExchangeRates.Core.RateConversion;
using ExchangeRates.Test.CoreTesting.RateConversionTesting.Helpers;
using Xunit;

namespace ExchangeRates.Test.CoreTesting.RateConversionTesting
{
    public class TestingRateConversion
    {

        [Fact]
        public void ConvertBetweenTwoCur_OnSuccess_ExpectDecimal_100()
        {
            var res = OfflineRateConversion.ConvertBetweenTwoCur(10m, 10m);

            Assert.True(res == 100);
        }

        [Fact]
        public void ConvertBetweenTwoCur_OnFailureZeroRate_ExpectNull()
        {
            var res = OfflineRateConversion.ConvertBetweenTwoCur(0m, 10m);

            Assert.Null(res);
        }

        [Fact]
        public void ConvertBetweenTwoCur_OnFailureNegativeRate_ExpectNull()
        {
            var res = OfflineRateConversion.ConvertBetweenTwoCur(-10m, 10m);

            Assert.Null(res);
        }


        [Fact]
        public void ConvertBetweenTwoCur_OnFailureZeroAmount_ExpectNull()
        {
            var res = OfflineRateConversion.ConvertBetweenTwoCur(10m, 0m);

            Assert.Null(res);
        }

        [Fact]
        public void ConvertBetweenTwoCur_OnFailureNegativeAmount_ExpectNull()
        {
            var res = OfflineRateConversion.ConvertBetweenTwoCur(10m, -0.1m);

            Assert.Null(res);
        }


        [Fact]
        public void ConvertWithMiddlePrice_OnSuccess_ExpectDecimal_0_96()
        {
            var rates = RatesHelper.GenerateRates();
            var middlePrice = OfflineRateConversion.GenerateMiddlePrice(rates, "USD", "EUR", 1m);

            Assert.True(middlePrice == 0.96m);
        }

        [Fact]
        public void ConvertWithMiddlePrice_OnSuccess_ExpectDecimal_117_33()
        {
            var rates = RatesHelper.GenerateRates();
            var middlePrice = OfflineRateConversion.GenerateMiddlePrice(rates, "EUR", "RSD", 1m);

            Assert.True(middlePrice == 117.33333333333333333333333333m);
        }

        [Fact]
        //Symbol not found
        public void ConvertWithMiddlePrice_OnFailureSymbols_ExpectNull()
        {
            var rates = RatesHelper.GenerateRates();
            var middlePrice = OfflineRateConversion.GenerateMiddlePrice(rates, "ERRORSYM", "RSD", 1m);

            Assert.Null(middlePrice);
        }

        [Fact]
        //Symbol not found
        public void ConvertWithMiddlePrice_OnFailureZeroAmount_ExpectNull()
        {
            var rates = RatesHelper.GenerateRates();
            var middlePrice = OfflineRateConversion.GenerateMiddlePrice(rates, "EUR", "RSD", 0m);

            Assert.Null(middlePrice);
        }

        [Fact]
        //Symbol not found
        public void ConvertWithMiddlePrice_OnFailureNegativeAmount_ExpectNull()
        {
            var rates = RatesHelper.GenerateRates();
            var middlePrice = OfflineRateConversion.GenerateMiddlePrice(rates, "EUR", "RSD", -0.1m);

            Assert.Null(middlePrice);
        }

    }
}
