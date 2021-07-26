using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Configs;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers.admin
{
    public abstract class AdminAuthorizationNeededRequest<T>
    {
        protected HttpRequest _request;
        protected CustomerContext _context;
        protected RentContext _rentContext;
        protected CarContext _carContext;

        protected AdminAuthorizationNeededRequest(HttpRequest request, CustomerContext context, RentContext rentContext, CarContext carContext)
        {
            _request = request;
            _context = context;
            _rentContext = rentContext;
            _carContext = carContext;
        }

        public abstract Task<bool> Handle(T request);

        public Task<bool> AdminHandle(T request)
        {
            var adminSecret = _request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (adminSecret == null || !adminSecret.Equals(ConfigValues.AdminSecret))
            {
                return Task.FromResult<bool>(false);
            }
            return Handle(request);
        }
    }
}
