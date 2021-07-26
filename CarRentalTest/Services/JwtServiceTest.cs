using CarRental.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRentalTest.Services
{
    [TestClass]
    public class JwtServiceTest
    {
        [TestMethod]
        public void ShouldEncodeAndDecodeEmailFromToken()
        {
            var mockEmail = "test123@gmail.com";
            string token = JwtService.GenerateToken(mockEmail);
            string decodedEmail = JwtService.DecodeEmailFromToken(token);

            Assert.AreEqual(mockEmail, decodedEmail);
        }
    }
}
