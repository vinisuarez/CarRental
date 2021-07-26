using System;
using CarRental.Models.Cars;

namespace CarRental.Models.Api.Responses
{
    public class CalculateResponse
    {
        public Car Car { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
    }
}
