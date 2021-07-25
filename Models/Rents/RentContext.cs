using System.Collections.Generic;
using System.Linq;
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

        public List<Rent> FindAllForCustomer(string customerId)
        {
            return Rents.Where(r => r.CustomerId == customerId).ToList();
        }
    }
}
