using System;
using System.Collections.Generic;
using CarRental.Models.Cars;

namespace CarRental.Models.Api.Responses
{
    public class ListCarsResponse
    {
        public List<PublicCar> Cars { get; set; }
    }

    public class PublicCar
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarType Type { get; set; }
        public int Bonus { get; set; }
        public bool IsMaintenance { get; set; }
        public bool IsRented { get; set; }
        public DateTime RentEnd { get; set; }
    }
}
