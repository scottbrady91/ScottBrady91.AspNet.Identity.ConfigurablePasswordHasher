using System;
using Microsoft.AspNet.Identity;

namespace ScottBrady91.AspNet.Identity.ConfigurablePasswordHasher
{
    public class ConfigurablePasswordHasher : IPasswordHasher
    {
        private readonly int iterationCount;

        public ConfigurablePasswordHasher(int iterationCount = 10000)
        {
            if (iterationCount < 1)
            {
                throw new ArgumentOutOfRangeException("iterationCount", "Password has iteration count cannot be less than 1");
            }

            this.iterationCount = iterationCount;
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password, iterationCount);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword, iterationCount))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}