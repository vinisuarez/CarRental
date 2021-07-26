using System;
using CarRental.Configs;
using CarRental.Models.Api.Responses;
using CarRental.Models.Cars;

namespace CarRental.Services
{
    public class RentCalculatorService
    {
        // could improve to calculate price based on the desired currency
        public static CalculateResponse RentCalculate(Car car, int days)
        {
            var priceCurrency = "EUR";
            decimal priceAmount = 0.0m;

            switch (car.Type)
            {
                case CarType.Convertible:
                    {
                        priceAmount = CalculatePrice(
                            ConfigValues.PremiunPrice,
                            ConfigValues.ConvertibleDaysForDiscount,
                            ConfigValues.ConvertibleDiscountPercentile,
                            days
                        );
                        break;
                    }
                case CarType.MiniVan:
                    {
                        priceAmount = CalculatePrice(
                            ConfigValues.BasicPrice,
                            ConfigValues.MiniVanDaysForDiscount,
                            ConfigValues.MiniVanDiscountPercentile,
                            days
                        );
                        break;
                    }
                case CarType.Suv:
                    {
                        priceAmount = CalculatePrice(
                            ConfigValues.BasicPrice,
                            ConfigValues.SuvDaysForDiscount,
                            ConfigValues.SuvDiscountPercentile,
                            days
                        );
                        break;
                    }
            }
            return new CalculateResponse
            {
                Car = car,
                PriceCurrency = priceCurrency,
                PriceAmount = priceAmount
            };

        }

        public static CalculateResponse SurchargeCalculate(Car car, int days)
        {
            var priceCurrency = "EUR";
            decimal priceAmount;
            if (car.Type == CarType.Convertible)
            {
                priceAmount = days * ConfigValues.PremiunPrice;
            }
            else
            {
                priceAmount = days * ConfigValues.BasicPrice;
            }

            return new CalculateResponse
            {
                Car = car,
                PriceCurrency = priceCurrency,
                PriceAmount = priceAmount
            };

        }


        public static decimal CalculatePrice(int basePrice, int minDayForDiscount, int percentageDiscount, int days)
        {
            if (minDayForDiscount != 0 && days > minDayForDiscount)
            {
                var daysWithDiscount = days - minDayForDiscount;
                var discountedPrice = basePrice - basePrice * ((decimal)percentageDiscount / 100);
                return (basePrice * minDayForDiscount) + (discountedPrice * daysWithDiscount);
            }
            else
            {
                return basePrice * days;

            }
        }
    }
}
