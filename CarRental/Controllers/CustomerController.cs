using System.Threading.Tasks;
using CarRental.Handlers;
using CarRental.Models.Api.Requests;
using CarRental.Models.Api.Responses;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using CarRental.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/carrental/v1/customer")]
    public class CustomerController : Controller
    {
        private readonly CustomerContext _context;
        private readonly RentContext _rentContext;
        private readonly TransactionContext _transactionContext;

        public CustomerController(CustomerContext context, RentContext rentContext, TransactionContext transactionContext)
        {
            _context = context;
            _rentContext = rentContext;
            _transactionContext = transactionContext;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterRequest request)
        {
            var registerHandler = new RegisterHandler(_context);
            if (await registerHandler.Handle(request))
            {
                return true;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var loginHandler = new LoginHandler(_context);
            LoginResponse loginResponse = await loginHandler.Handle(request);
            if (loginResponse != null)
            {
                return loginResponse;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("details")]
        [HttpGet]
        public async Task<ActionResult<DetailsResponse>> Details()
        {
            var detailHandler = new DetailsHandler(_context, Request);
            DetailsResponse detailsResponse = await detailHandler.Handle();
            if (detailsResponse != null)
            {
                return detailsResponse;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("rentHistory")]
        [HttpGet]
        public async Task<ActionResult<RentHistoryResponse>> RentHistory()
        {
            var rentHistoryHandler = new RentHistoryHandler(_context, Request, _rentContext, _transactionContext);
            RentHistoryResponse rentHistoryResponse = await rentHistoryHandler.Handle();
            if (rentHistoryResponse != null)
            {
                return rentHistoryResponse;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
