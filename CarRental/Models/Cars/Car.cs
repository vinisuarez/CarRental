namespace CarRental.Models.Cars
{
    public class Car
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public CarType Type { get; set; }
        public int Bonus { get; set; }

        public bool InMaintenance { get; set; }
    }
}
