using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Models.Api.Responses;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using CarRental.Models.Transactions;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class RentHistoryHandler : AuthorizationNeededHandler
    {
        private RentContext _rentContext;
        private TransactionContext _transactionContext;


        public RentHistoryHandler(CustomerContext context, HttpRequest request, RentContext rentContext, TransactionContext transactionContext)
            : base(context, request)
        {
            _rentContext = rentContext;
            _transactionContext = transactionContext;
        }

        public async Task<RentHistoryResponse> Handle()
        {
            var customer = await loadCustomer;
            if (customer != null)
            {
                List<Rent> customerRents = await _rentContext.FindAllForCustomer(customer.Email);
                List<Transaction> customerTransactions = await _transactionContext.FindAllForCustomer(customer.Email);

                return new RentHistoryResponse
                {
                    CustomerRents = customerRents,
                    Transactions = customerTransactions
                };
            }
            else
            {
                return null;
            }
        }
    }
}
