using System;
namespace CarRental.Models.Api.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
