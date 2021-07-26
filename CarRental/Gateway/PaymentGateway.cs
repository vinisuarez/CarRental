using System;
using CarRental.Models.Customers;

namespace CarRental.Gateway
{
    public class PaymentGateway
    {
        // Here there should be a real call to some payment gateway service to charge the customer
        public static bool FakePaymenteGateway(Customer customer, decimal price, string currency)
        {
            var paymentResult = true; // PaymentGateway.proccess(customer, price, currency);
            return paymentResult;
        }
    }
}
