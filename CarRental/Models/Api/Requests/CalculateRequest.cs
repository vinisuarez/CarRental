namespace CarRental.Models.Api.Requests
{
    public class CalculateRequest
    {
        public string CarId { get; set; }
        public int Days { get; set; }
    }
}
