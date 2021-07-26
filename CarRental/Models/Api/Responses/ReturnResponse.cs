using CarRental.Models.Rents;

namespace CarRental.Models.Api.Responses
{
    public class ReturnResponse
    {
        public Rent ReturnedRent { get; set; }
        public Transaction Surcharge { get; set; }
    }
}
