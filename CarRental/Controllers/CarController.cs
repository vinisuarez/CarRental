using System;
using System.Threading.Tasks;
using CarRental.Handlers;
using CarRental.Models.Api.Requests;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using CarRental.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/carrental/v1/cars")]
    public class CarController : Controller
    {
        private readonly CustomerContext _context;
        private readonly RentContext _rentContext;
        private readonly CarContext _carContext;
        private readonly TransactionContext _transactionContext;

        public CarController(CustomerContext context, RentContext rentContext, CarContext carContext, TransactionContext transactionContext)
        {
            _context = context;
            _rentContext = rentContext;
            _carContext = carContext;
            _transactionContext = transactionContext;
        }

        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<ListCarsResponse>> Details()
        {
            var listCarsHandler = new ListCarsHandler(_context, Request, _rentContext, _carContext);
            ListCarsResponse listCarsResponse = await listCarsHandler.Handle();
            if (listCarsResponse != null)
            {
                return listCarsResponse;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("calculate")]
        [HttpPost]
        public async Task<ActionResult<CalculateResponse>> Calculate([FromBody] CalculateRequest request)
        {
            var calculateHandler = new CalculateHandler(_context, Request, _carContext);
            CalculateResponse calculateResponse = await calculateHandler.Handle(request);
            if (calculateResponse != null)
            {
                return calculateResponse;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("rent")]
        [HttpPost]
        public async Task<ActionResult<RentResponse>> Rent([FromBody] RentRequest request)
        {
            var rentHandler = new RentHandler(_context, Request, _rentContext, _carContext, _transactionContext);
            RentResponse rentResponse = await rentHandler.Handle(request);
            if (rentResponse != null)
            {
                return rentResponse;
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("return")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> Return([FromBody] ReturnRequest request)
        {
            var returnHandler = new ReturnHandler(_context, Request, _rentContext, _carContext, _transactionContext);
            ReturnResponse returnResponse = await returnHandler.Handle(request);
            if (returnResponse != null)
            {
                return returnResponse;
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
