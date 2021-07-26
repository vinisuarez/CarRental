using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Models.Customers;
using CarRental.Services;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class AuthorizationNeededHandler
    {
        protected CustomerContext _context;
        protected HttpRequest _request;
        protected Task<Customer> loadCustomer;

        public AuthorizationNeededHandler(CustomerContext context, HttpRequest request)
        {
            _context = context;
            _request = request;
            loadCustomer = GetCustomerAsync();
        }

        private async Task<Customer> GetCustomerAsync()
        {
            var token = _request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return null;
            }

            var email = JwtService.DecodeEmailFromToken(token);
            return await _context.Customers.FindAsync(email);
        }
    }
}
