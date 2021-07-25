using System;
using System.Collections.Generic;

namespace CarRental.Models.Rents
{
    public class Rent
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CarId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<Transaction> TransactionLogs { get; set; }
    }
}
