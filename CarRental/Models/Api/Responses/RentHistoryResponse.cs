using CarRental.Models.Rents;
using System.Collections.Generic;

namespace CarRental.Models.Api.Responses
{
    public class RentHistoryResponse
    {
        public List<Rent> CustomerRents { get; set; }
    }
}
