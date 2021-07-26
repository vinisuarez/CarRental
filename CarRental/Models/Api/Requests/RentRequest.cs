using System;
using System.Collections.Generic;

namespace CarRental.Models.Api.Requests
{
    public class RentRequest
    {
        public List<RentRequestData> RentRequests { get; set; }
    }

    public class RentRequestData
    {
        public string CarId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
