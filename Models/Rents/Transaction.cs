namespace CarRental.Models.Rents
{
    public class Transaction
    {
        public string Id { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public string Token { get; set; }
        public TransactionType Type { get; set; }
    }
}
