using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Adita.Identity.Core.Options;

namespace Adita.Identity.Core.Test.Options
{
    [TestClass]
    public class ApplicationIdentityOptionsTest
    {
        [TestMethod]
        public void VerifyDefaultTest()
        {
            ApplicationIdentityOptions options = new ApplicationIdentityOptions();

            Assert.AreEqual(options.EmailClaimType, ClaimTypes.Email);
            Assert.AreEqual(options.RoleClaimType, ClaimTypes.Role);
            Assert.AreEqual(options.UserIdClaimType, ClaimTypes.NameIdentifier);
            Assert.AreEqual(options.UserNameClaimType, ClaimTypes.Name);
        }
        [TestMethod]
        public void CanCustomize()
        {
            var services = new ServiceCollection().Configure<ApplicationIdentityOptions>(
                options =>
                {
                    options.UserIdClaimType = "id";
                    options.RoleClaimType = "role";
                    options.UserNameClaimType = "username";
                    options.EmailClaimType = "email";
                });

            var serviceProvider = services.BuildServiceProvider();

            var setup = serviceProvider.GetRequiredService<IConfigureOptions<ApplicationIdentityOptions>>();
            Assert.IsNotNull(setup);

            var optionsGetter = serviceProvider.GetRequiredService<IOptions<ApplicationIdentityOptions>>();
            Assert.IsNotNull(optionsGetter);

            var options = optionsGetter.Value;
            Assert.IsTrue(options.UserIdClaimType == "id");
            Assert.IsTrue(options.RoleClaimType == "role");
            Assert.IsTrue(options.UserNameClaimType == "username");
            Assert.IsTrue(options.EmailClaimType == "email");

        }
    }
}
