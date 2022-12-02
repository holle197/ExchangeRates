namespace ExchangeRates.Web.Models.Currencies
{
    public class CurrenciesModel
    {
        public bool Success { get; set; }
        public string? ErrorMsg { get; set; }
        public List<CurrencyModel> Currencies { get; set; } = new();
    }
}
