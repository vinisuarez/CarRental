using System;
namespace CarRental.Models.Api.Responses
{
    public class DetailsResponse
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreateAt { get; set; }
        public int Bonus { get; set; }
    }
}
