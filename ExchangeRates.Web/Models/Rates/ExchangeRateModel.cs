namespace ExchangeRates.Web.Models.Rates
{
    public class ExchangeRateModel
    {
        public bool Success { get; set; }
        public string? ErrorMsg { get; set; }
        public string LatesUpdate { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Result { get; set; }
    }
}
