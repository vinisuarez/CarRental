using CarRental.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRentalTest.Services
{
    [TestClass]
    public class RentCalculatorServiceTest
    {
        [TestMethod]
        public void ShouldCalculatePriceForConvertible()
        {
            decimal price = RentCalculatorService.CalculatePrice(150, 0, 0, 1);
            Assert.AreEqual(150m, price);
        }

        [TestMethod]
        public void ShouldCalculatePriceFoMiniVan()
        {
            decimal price = RentCalculatorService.CalculatePrice(100, 5, 20, 6);
            Assert.AreEqual(580m, price);
        }

        [TestMethod]
        public void ShouldCalculatePriceForMiniVan2()
        {
            decimal price = RentCalculatorService.CalculatePrice(100, 5, 20, 2);
            Assert.AreEqual(200m, price);
        }

        [TestMethod]
        public void ShouldCalculatePriceForSuv()
        {
            decimal price = RentCalculatorService.CalculatePrice(100, 3, 30, 7);
            Assert.AreEqual(580m, price);
        }
    }
}