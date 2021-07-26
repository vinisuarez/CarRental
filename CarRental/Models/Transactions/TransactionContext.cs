using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Models.Rents;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Models.Transactions
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        public Task<List<Transaction>> FindAllForCustomer(string customerId)
        {
            return Transactions.Where(t => t.CustomerId == customerId).ToListAsync();
        }
    }
}
