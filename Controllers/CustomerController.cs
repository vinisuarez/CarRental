using System.Threading.Tasks;
using CarRental.helper;
using CarRental.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/carrental/v1/customer")]
    public class CustomerController : Controller
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Customer>> Register([FromBody] Customer customer)
        {
            var customerFind = await _context.Customers.FindAsync(customer.UserName);

            if (customerFind == null)
            {
                var salt = PasswordHelper.GenerateSalt();
                customer.Salt = salt;
                customer.Password = PasswordHelper.HashPassword(customer.Password, salt);
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<Customer>> Login([FromBody] Customer customer)
        {
            var customerFind = await _context.Customers.FindAsync(customer.UserName);

            if (customerFind != null)
            {
                var hashedPassword = PasswordHelper.HashPassword(customer.Password, customerFind.Salt);

                if (hashedPassword.Equals(customerFind.Password)) 
                {
                    return customer;
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [Route("details")]
        [HttpPost]
        public void Details([FromBody] string value)
        {
        }
    }
}
