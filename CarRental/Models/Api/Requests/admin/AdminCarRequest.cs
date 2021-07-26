using CarRental.Models.Api.Requests.admin;
using CarRental.Models.Cars;

namespace CarRental.Models.Api.Requests
{
    public class AdminCarRequest : AdminBaseRequest
    {
        public string Id { get; set; }
    }
}
