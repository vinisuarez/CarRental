using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Customers;
using CarRental.Services;

namespace CarRental.Handlers
{
    public class RegisterHandler
    {
        private CustomerContext _context;

        public RegisterHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RegisterRequest request)
        {
            var customerFind = await _context.Customers.FindAsync(request.Email);

            if (customerFind == null)
            {
                Customer customer = CreateCustomer(request);

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Customer CreateCustomer(RegisterRequest request)
        {
            var salt = PasswordService.GenerateSalt();
            var password = PasswordService.HashPassword(request.Password, salt);

            var customer = new Customer
            {
                Email = request.Email,
                Address = request.Address,
                Bonus = 0,
                CreateAt = System.DateTime.UtcNow,
                Salt = salt,
                Password = password,
                Blocked = false
            };
            return customer;
        }
    }
}
