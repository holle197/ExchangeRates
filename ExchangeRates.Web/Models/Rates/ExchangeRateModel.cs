namespace ExchangeRates.Web.Models.Rates
{
    public class ExchangeRateModel
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string LatesUpdate { get; set; } = string.Empty;
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Result { get; set; }
    }
}
