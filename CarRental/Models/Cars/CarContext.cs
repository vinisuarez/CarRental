﻿using Microsoft.EntityFrameworkCore;

namespace CarRental.Models.Cars
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        { 
        }

        public DbSet<Car> Cars { get; set; }
    }
}
