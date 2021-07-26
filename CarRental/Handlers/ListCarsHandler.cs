using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class ListCarsHandler : AuthorizationNeededHandler
    {
        private RentContext _rentContext;
        private CarContext _carContext;

        public ListCarsHandler(CustomerContext context, HttpRequest request, RentContext rentContext, CarContext carContext)
            : base(context, request)
        {
            _rentContext = rentContext;
            _carContext = carContext;
        }

        public async Task<ListCarsResponse> Handle()
        {
            var customerFind = await GetCustomerAsync();
            if (customerFind != null)
            {
                List<PublicCar> publicCars = new();
                List<Car> cars = await _carContext.Cars.ToListAsync();

                foreach (var car in cars)
                {
                    var publicCar = new PublicCar
                    {
                        Id = car.Id,
                        Brand = car.Brand,
                        Model = car.Model,
                        Type = car.Type,
                        Bonus = car.Bonus,
                        IsMaintenance = car.InMaintenance
                    };

                    if (!String.IsNullOrEmpty(car.RentId))
                    {
                        var rent = await _rentContext.Rents.FindAsync(car.RentId);

                        publicCar.IsRented = true;
                        publicCar.RentEnd = rent.EndAt;
                    }
                    publicCars.Add(publicCar);
                }
                return new ListCarsResponse
                {
                    Cars = publicCars
                };
            }
            else
            {
                return null;
            }
        }
    }
}
