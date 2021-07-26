using System.Threading.Tasks;
using CarRental.Models.Api.Requests;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers.admin
{
    public class AdminMaintenanceCarHandler : AdminAuthorizationNeededRequest<AdminCarRequest>
    {
        public AdminMaintenanceCarHandler(HttpRequest request, CustomerContext context, RentContext rentContext, CarContext carContext)
            : base(request, context, rentContext, carContext)
        {
        }

        public async override Task<bool> Handle(AdminCarRequest request)
        {
            Car car = await _carContext.Cars.FindAsync(request.Id);
            if (car == null)
            {
                return false;
            }
            else
            {
                car.InMaintenance = true;
                _carContext.Cars.Update(car);
                await _carContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
