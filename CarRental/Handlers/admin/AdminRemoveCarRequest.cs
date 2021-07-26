using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers.admin
{
    public class AdminRemoveCarHandler : AdminAuthorizationNeededRequest<AdminCarRequest>
    {
        public AdminRemoveCarHandler(HttpRequest request, CustomerContext context, RentContext rentContext, CarContext carContext)
            : base(request, context, rentContext, carContext)
        {
        }

        public async override Task<bool> Handle(AdminCarRequest request)
        {
            Car car = await _carContext.Cars.FindAsync(request.Id);
            Rent rent = await _rentContext.FindActiveForCar(car.Id);
            // can't remove cars that are rented
            if (car == null || rent != null)
            {
                return false;
            }
            else
            {
                _carContext.Cars.Remove(car);
                await _carContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
