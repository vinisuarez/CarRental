using System;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/carrental/v1/admin")]
    public class AdminController : Controller
    {
        private readonly CustomerContext _context;
        private readonly RentContext _rentContext;
        private readonly CarContext _carContext;

        public AdminController(CustomerContext context, RentContext rentContext, CarContext carContext)
        {
            _context = context;
            _rentContext = rentContext;
            _carContext = carContext;
        }


    }
}
