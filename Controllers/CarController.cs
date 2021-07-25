using System;
using System.Threading.Tasks;
using CarRental.Handlers;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/carrental/v1/cars")]
    public class CarController : Controller
    {
        private readonly CustomerContext _context;
        private readonly RentContext _rentContext;
        private readonly CarContext _carContext;

        public CarController(CustomerContext context, RentContext rentContext, CarContext carContext)
        {
            _context = context;
            _rentContext = rentContext;
            _carContext = carContext;
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

    }
}
