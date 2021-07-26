using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Gateway;
using CarRental.Models.Api.Requests;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;
using CarRental.Models.Customers;
using CarRental.Models.Rents;
using CarRental.Models.Transactions;
using CarRental.Services;
using Microsoft.AspNetCore.Http;

namespace CarRental.Handlers
{
    public class RentHandler : AuthorizationNeededHandler
    {
        private RentContext _rentContext;
        private CarContext _carContext;
        private TransactionContext _transactionContext;

        public RentHandler(CustomerContext context, HttpRequest request, RentContext rentContext, CarContext carContext, TransactionContext transactionContext)
            : base(context, request)
        {
            _rentContext = rentContext;
            _carContext = carContext;
            _transactionContext = transactionContext;
        }

        public async Task<RentResponse> Handle(RentRequest request)
        {
            var customer = await loadCustomer;
            if (customer != null)
            {
                var totalPriceAmount = 0.0m;
                var totalPriceCurrency = "EUR";
                var totalBonus = 0;

                List<Rent> successRents = new();
                List<Transaction> successTransactions = new();


                foreach (var rentRequest in request.RentRequests)
                {
                    Car car = await _carContext.Cars.FindAsync(rentRequest.CarId);
                    if (car == null || car.InMaintenance)
                    {
                        return null;
                    }
                    Rent activeRent = await _rentContext.FindActiveForCarWithDate(rentRequest.CarId, rentRequest.StartAt, rentRequest.EndAt);
                    if (activeRent != null)
                    {
                        // can not rent if the car is already rented for within the dates
                        return null;
                    }

                    int days = (rentRequest.EndAt - rentRequest.StartAt).Days;
                    if (days < 1)
                    {
                        return null;
                    }

                    CalculateResponse rentCalculation = RentCalculatorService.RentCalculate(car, days);
                    totalPriceAmount += rentCalculation.PriceAmount;
                    totalBonus += car.Bonus;

                    Rent rent = BuildNewRent(rentRequest, customer.Email);
                    successRents.Add(rent);

                    Transaction transaction = BuildNewTransaction(rent.Id, customer.Email, rentCalculation.PriceAmount, rentCalculation.PriceCurrency, TransactionType.Intial);
                    successTransactions.Add(transaction);
                }

                bool paymentResult = PaymentGateway.FakePaymenteGateway(customer, totalPriceAmount, totalPriceCurrency);
                if (!paymentResult)
                {
                    return null;
                }

                customer.Bonus += totalBonus;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                await _rentContext.Rents.AddRangeAsync(successRents);
                await _transactionContext.Transactions.AddRangeAsync(successTransactions);

                await _transactionContext.SaveChangesAsync();
                await _rentContext.SaveChangesAsync();

                return new RentResponse
                {
                    RentRequests = request.RentRequests,
                    Transactions = successTransactions,
                    TotalPriceAmount = totalPriceAmount,
                    TotalPriceCurrency = totalPriceCurrency
                };
            }
            else
            {
                return null;
            }
        }

        private static Rent BuildNewRent(RentRequestData rentRequest, string customerId)
        {
            return new Rent
            {
                Id = Guid.NewGuid().ToString(),
                StartAt = rentRequest.StartAt,
                EndAt = rentRequest.EndAt,
                CarId = rentRequest.CarId,
                CustomerId = customerId,
                Returned = false
            };
        }

        public static Transaction BuildNewTransaction(string rentId, string customerId, decimal price, string currency, TransactionType type)
        {
            return new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                RentId = rentId,
                CustomerId = customerId,
                PriceAmount = price,
                PriceCurrency = currency,
                Type = type
            };

        }
    }
}
