using System;
using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers.admin
{
    public class AdminAddCarHandler : AdminAuthorizationNeededRequest<AdminAddCarRequest>
    {
        public AdminAddCarHandler(HttpRequest request, CustomerContext context, RentContext rentContext, CarContext carContext)
            : base(request, context, rentContext, carContext)
        {
        }

        public async override Task<bool> Handle(AdminAddCarRequest request)
        {
            var newCar = new Car
            {
                Id = Guid.NewGuid().ToString(),
                Brand = request.Brand,
                Model = request.Model,
                Type = request.Type,
                InMaintenance = false,
                Bonus = GenerateBonus(request.Type)
            };

            await _carContext.Cars.AddAsync(newCar);
            await _carContext.SaveChangesAsync();
            return true;
        }

        private int GenerateBonus(CarType carType)
        {
            switch(carType)
            {
                case CarType.Convertible:
                    {
                        return 2;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }
    }
}
