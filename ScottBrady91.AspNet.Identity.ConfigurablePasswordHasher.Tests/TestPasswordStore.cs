using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ScottBrady91.AspNet.Identity.ConfigurablePasswordHasher.Tests
{
    public class TestPasswordStore : IUserPasswordStore<TestUser>, IUserStore<TestUser>
    {
        private readonly Dictionary<string, TestUser> users;

        public TestPasswordStore()
        {
            users = new Dictionary<string, TestUser>();
        }

        public Task CreateAsync(TestUser user)
        {
            users.Add(user.Id, user);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TestUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TestUser user)
        {
            users.Remove(user.Id);
            return Task.FromResult(0);
        }

        public Task<TestUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(users[userId]);
        }

        public Task<TestUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(users.Values.FirstOrDefault(x => x.UserName == userName));
        }

        public Task SetPasswordHashAsync(TestUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TestUser user)
        {
            var testUser = users[user.Id];
            return Task.FromResult(testUser.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TestUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public void Dispose()
        {
        }
    }
}