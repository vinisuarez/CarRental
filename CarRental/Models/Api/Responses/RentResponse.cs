using System;
using System.Collections.Generic;
using CarRental.Models.Api.Requests;
using CarRental.Models.Rents;

namespace CarRental.Models.Api.Responses
{
    public class RentResponse
    {
        public List<RentRequestData> RentRequests { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal TotalPriceAmount { get; set; }
        public string TotalPriceCurrency { get; set; }
    }

    public class RentResponseData
    {
        public string CarId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
    }
}
