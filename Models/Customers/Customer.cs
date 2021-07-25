using System;
using System.ComponentModel.DataAnnotations;
namespace CarRental.Models.Customers
{
    public class Customer
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Address { get; set; }
        public DateTime CreateAt { get; set; }
        public int Bonus { get; set; }
        public bool Blocked{ get; set; }
    }
}