using Adita.Identity.Core.Models;
using Adita.Identity.Core.Services.PasswordHashers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adita.Identity.Core.Test.Services.PasswordHashers
{
    [TestClass]
    public class PasswordHasherTest
    {
        [TestMethod]
        public void VerifyConcistency()
        {
            string password = "TestPassword";

            IdentityUser user = new()
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser"
            };

            BCryptPasswordHasher<Guid, IdentityUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            Assert.IsTrue(verificationResult == PasswordVerificationResult.Success);

            PasswordVerificationResult verificationResult1 = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, "AAA");
            Assert.IsFalse(verificationResult1 == PasswordVerificationResult.Success);
        }
    }
}
