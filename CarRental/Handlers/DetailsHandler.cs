using System.Threading.Tasks;
using CarRental.Models.Api.Responses;
using CarRental.Models.Customers;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class DetailsHandler : AuthorizationNeededHandler
    {
        public DetailsHandler(CustomerContext context, HttpRequest request)
            : base(context, request)
        {
        }

        public async Task<DetailsResponse> Handle()
        {
            var customer = await loadCustomer;
            if (customer != null)
            {
                return new DetailsResponse
                {
                    Email = customer.Email,
                    Address = customer.Address,
                    CreateAt = customer.CreateAt,
                    Bonus = customer.Bonus,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
