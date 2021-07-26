using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers.admin
{
    public class AdminBlockCustomerHandler : AdminAuthorizationNeededRequest<AdminCustomerRequest>
    {

        private bool _block;

        public AdminBlockCustomerHandler(HttpRequest request, CustomerContext context, RentContext rentContext, CarContext carContext, bool block)
            : base(request, context, rentContext, carContext)
        {
            _block = block;
        }

        public async override Task<bool> Handle(AdminCustomerRequest request)
        {
            Customer customer = await _context.Customers.FindAsync(request.Id);
            if (customer == null)
            {
                return false;
            }
            else
            {
                customer.Blocked = _block;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
