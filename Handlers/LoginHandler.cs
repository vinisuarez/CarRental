using CarRental.Models.Api.Requests;
using CarRental.Models.Api.Responses;
using CarRental.Models.Customers;
using CarRental.Services;
using System.Threading.Tasks;


namespace CarRental.Handlers
{
    public class LoginHandler
    {
        private CustomerContext _context;

        public LoginHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Handle(LoginRequest request)
        {
            var customerFind = await _context.Customers.FindAsync(request.Email);
            if (customerFind != null)
            {
                if (customerFind.Blocked)
                {
                    return null;
                }
                var hashedPassword = PasswordService.HashPassword(request.Password, customerFind.Salt);
                if (hashedPassword.Equals(customerFind.Password))
                {
                    return new LoginResponse
                    {
                        Token = JwtService.GenerateToken(customerFind.Email)
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
