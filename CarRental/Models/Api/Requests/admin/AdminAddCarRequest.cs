using CarRental.Models.Api.Requests.admin;
using CarRental.Models.Cars;

namespace CarRental.Models.Api.Requests
{
    public class AdminAddCarRequest : AdminBaseRequest
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarType Type { get; set; }
    }
}
