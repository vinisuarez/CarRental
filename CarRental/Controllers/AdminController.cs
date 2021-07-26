using System;
using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using CarRental.Handlers.admin;
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

        [Route("car/add")]
        [HttpPost]
        public async Task<ActionResult<bool>> AddCar([FromBody] AdminAddCarRequest request)
        {
            var adminAddCarHandler = new AdminAddCarHandler(Request, _context, _rentContext, _carContext);
            bool success = await adminAddCarHandler.AdminHandle(request);

            if (success)
            {
                return true;
            }
            return BadRequest();

        }

        [Route("car/remove")]
        [HttpPost]
        public async Task<ActionResult<bool>> RemoveCar([FromBody] AdminCarRequest request)
        {
            var adminRemoveCarHandler = new AdminRemoveCarHandler(Request, _context, _rentContext, _carContext);
            bool success = await adminRemoveCarHandler.AdminHandle(request);

            if (success)
            {
                return true;
            }
            return BadRequest();

        }

        [Route("car/maintenance")]
        [HttpPost]
        public async Task<ActionResult<bool>> MainenanceCar([FromBody] AdminCarRequest request)
        {
            var adminMaintenanceCarHandler = new AdminMaintenanceCarHandler(Request, _context, _rentContext, _carContext);
            bool success = await adminMaintenanceCarHandler.AdminHandle(request);

            if (success)
            {
                return true;
            }
            return BadRequest();

        }

        [Route("customer/block")]
        [HttpPost]
        public async Task<ActionResult<bool>> BlockCustomer([FromBody] AdminCustomerRequest request)
        {
            var adminBlockCustomerHandler = new AdminBlockCustomerHandler(Request, _context, _rentContext, _carContext, true);
            bool success = await adminBlockCustomerHandler.AdminHandle(request);

            if (success)
            {
                return true;
            }
            return BadRequest();

        }

        [Route("customer/unblock")]
        [HttpPost]
        public async Task<ActionResult<bool>> UnblockCustomer([FromBody] AdminCustomerRequest request)
        {
            var adminBlockCustomerHandler = new AdminBlockCustomerHandler(Request, _context, _rentContext, _carContext, false);
            bool success = await adminBlockCustomerHandler.AdminHandle(request);

            if (success)
            {
                return true;
            }
            return BadRequest();

        }
    }
}
