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
            var customerFind = await GetCustomerAsync();
            if (customerFind != null)
            {
                return new DetailsResponse
                {
                    Email = customerFind.Email,
                    Address = customerFind.Address,
                    CreateAt = customerFind.CreateAt,
                    Bonus = customerFind.Bonus,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
