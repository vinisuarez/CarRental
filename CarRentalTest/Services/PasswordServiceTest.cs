using CarRental.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRentalTest.Services
{
    [TestClass]
    public class PasswordServiceTest
    {
        [TestMethod]
        public void ShouldHashPassword()
        {
            var mockSalt = "KA/sQZP4mFqZmkUmbpr6yg==";
            var mockPass = "testPass";
            string hashPassword = PasswordService.HashPassword(mockPass, mockSalt);
            Assert.AreEqual("cHmOD36PlyWHTRhfX/zHWxs9hKodOOEY9VEFMxpPzCE=", hashPassword);
        }
    }
}
