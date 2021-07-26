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
    public class ReturnHandler : AuthorizationNeededHandler
    {
        private RentContext _rentContext;
        private CarContext _carContext;
        private TransactionContext _transactionContext;

        public ReturnHandler(CustomerContext context, HttpRequest request, RentContext rentContext, CarContext carContext, TransactionContext transactionContext)
            : base(context, request)
        {
            _rentContext = rentContext;
            _carContext = carContext;
            _transactionContext = transactionContext;
        }

        public async Task<ReturnResponse> Handle(ReturnRequest request)
        {
            var customer = await loadCustomer;
            if (customer != null)
            {
                Rent rent = await _rentContext.Rents.FindAsync(request.RentId);
                if (rent == null)
                {
                    return null;
                }

                int delayedDays = (DateTime.UtcNow - rent.EndAt).Days;

                if (delayedDays > 0)
                {
                    Car car = await _carContext.Cars.FindAsync(rent.CarId);
                    var surchargeFee = RentCalculatorService.SurchargeCalculate(car, delayedDays);

                    bool paymentResult = PaymentGateway.FakePaymenteGateway(customer, surchargeFee.PriceAmount, surchargeFee.PriceCurrency);
                    if (!paymentResult)
                    {
                        return null;
                    }

                    Transaction transaction = RentHandler.BuildNewTransaction(rent.Id, customer.Email, surchargeFee.PriceAmount, surchargeFee.PriceCurrency, TransactionType.Late);

                    await _transactionContext.Transactions.AddAsync(transaction);
                    await _transactionContext.SaveChangesAsync();

                    return await updateRentAndResponse(rent, transaction);
                }
                else
                {
                    return await updateRentAndResponse(rent, null);
                }
            }
            else
            {
                return null;
            }
        }

        private async Task<ReturnResponse> updateRentAndResponse(Rent rent, Transaction transaction)
        {
            rent.Returned = true;
            _rentContext.Rents.Update(rent);
            await _rentContext.SaveChangesAsync();

            return new ReturnResponse
            {
                ReturnedRent = rent,
                Surcharge = transaction
            };
        }
    }
}
