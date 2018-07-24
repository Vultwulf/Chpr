using Chpr.DataLayer;
using Chpr.DataLayer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chpr.Tests
{
    [TestClass]
    public class AuthTest
    {
        ///<summary>
        /// Use known user credentials to determine if a match can be found.
        ///</summary>
        [TestMethod]
        public void AuthenticateKnownUser()
        {
            UsersService usersService = new UsersService();

            // Sample input
            string testUsername = "User1";
            string testPassword = "Password1";
            string encodedPassword = usersService.EncodePassword(testPassword);

            User user = usersService.GetUser(testUsername);

            // This should not be null
            Assert.IsNotNull(user);

            // If the user was found, match the password
            if (user != null)
            {
                Assert.AreEqual(encodedPassword, user.Password);
            }
        }
    }
}
