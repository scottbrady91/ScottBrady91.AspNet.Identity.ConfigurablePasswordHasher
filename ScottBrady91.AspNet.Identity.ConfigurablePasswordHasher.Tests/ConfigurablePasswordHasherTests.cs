using System;
using Microsoft.AspNet.Identity;
using Xunit;

namespace ScottBrady91.AspNet.Identity.ConfigurablePasswordHasher.Tests
{
    public class ConfigurablePasswordHasherTests
    {
        [Fact]
        public void WhenPasswordAndThenVerified_ExpectSuccessfullyVerified()
        {
            // arrange
            var password = Guid.NewGuid().ToString();
            var passwordHasher = new ConfigurablePasswordHasher(50000);

            // act
            var hash = passwordHasher.HashPassword(password);
            var result = passwordHasher.VerifyHashedPassword(hash, password);

            // assert
            Assert.Equal(PasswordVerificationResult.Success, result);
        }

        [Fact]
        public void WhenPasswordHashedTwice_ExpectNotEqual()
        {
            // arrange
            var password = Guid.NewGuid().ToString();
            var passwordHasher = new ConfigurablePasswordHasher(25000);

            // act
            var firstHash = passwordHasher.HashPassword(password);
            var secondHash = passwordHasher.HashPassword(password);

            // assert
            Assert.NotEqual(firstHash, secondHash);
        }

        [Fact]
        public void WhenPasswordCreatedAndCheckedUsingUserManager_ExpectSuccess()
        {
            // arrange
            var user = new TestUser { UserName = "scott@scottbrady91.com" };
            var password = Guid.NewGuid().ToString();

            var hasher = new ConfigurablePasswordHasher();
            var store = new TestPasswordStore();
            var manager = new UserManager<TestUser>(store) { PasswordHasher = hasher };

            // act & assert
            var creationResult = manager.Create(user, password);
            Assert.True(creationResult.Succeeded);

            var foundUser = manager.FindById(user.Id);
            Assert.True(manager.CheckPassword(foundUser, password));
        }
    }
}