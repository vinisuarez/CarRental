using System;
using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Services;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class CalculateHandler : AuthorizationNeededHandler
    {
        private CarContext _carContext;

        public CalculateHandler(CustomerContext context, HttpRequest request, CarContext carContext)
            : base(context, request)
        {
            _carContext = carContext;
        }

        public async Task<CalculateResponse> Handle(CalculateRequest request)
        {
            var customer = await loadCustomer;
            if (customer != null)
            {
                Car car = await _carContext.Cars.FindAsync(request.CarId);
                return RentCalculatorService.RentCalculate(car, request.Days);
            }
            else
            {
                return null;
            }
        }
    }
}
