using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Models.Api.Responses;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class RentHistoryHandler : AuthorizationNeededHandler
    {
        private RentContext _rentContext;
        public RentHistoryHandler(CustomerContext context, HttpRequest request, RentContext rentContext)
            : base(context, request)
        {
            _rentContext = rentContext;
        }

        public async Task<RentHistoryResponse> Handle()
        {
            var customerFind = await GetCustomerAsync();
            if (customerFind != null)
            {
                List<Rent> customerRents = await _rentContext.FindAllForCustomer(customerFind.Email);
                return new RentHistoryResponse
                {
                    CustomerRents = customerRents
                };
            }
            else
            {
                return null;
            }
        }
    }
}
