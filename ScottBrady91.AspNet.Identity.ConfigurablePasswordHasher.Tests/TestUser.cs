using System;
using Microsoft.AspNet.Identity;

namespace ScottBrady91.AspNet.Identity.ConfigurablePasswordHasher.Tests
{
    public class TestUser : IUser
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
