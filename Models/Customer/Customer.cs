using System;
using System.ComponentModel.DataAnnotations;
namespace CarRental.Models.Customer
{
    public class Customer
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreateAt { get; set; }
        public int Bonus { get; set; }
        public bool Blocked{ get; set; }
    }
}
