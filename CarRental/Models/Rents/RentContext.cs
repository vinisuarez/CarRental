using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Models.Rents
{
    public class RentContext : DbContext
    {
        public RentContext(DbContextOptions<RentContext> options)
            : base(options)
        {
        }

        public DbSet<Rent> Rents { get; set; }

        public Task<List<Rent>> FindAllForCustomer(string customerId)
        {
            return Rents.Where(r => r.CustomerId == customerId).ToListAsync();
        }

        public Task<Rent> FindActiveForCar(string carId)
        {
            return Rents.SingleOrDefaultAsync(r =>
                r.CarId == carId &&
                r.StartAt <= DateTime.UtcNow &&
                r.EndAt > DateTime.UtcNow &&
                r.Returned == false
            );
        }

        public Task<Rent> FindActiveForCarWithDate(string carId, DateTime start, DateTime end)
        {
            return Rents.SingleOrDefaultAsync(r =>
                r.CarId == carId &&
                (start < r.StartAt && end > r.StartAt || start > r.StartAt && (start < r.EndAt || r.Returned == false))
            );
        }
    }
}
