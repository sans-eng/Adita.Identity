using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Adita.Extensions.Logging;
using Adita.Identity.Core.Services;
using Adita.Identity.Core.Services.Managers.RoleManagers;
using Adita.Identity.Core.Services.Managers.UserManagers;
using Adita.Identity.Core.Services.Repositories.RoleClaimRepositories;
using Adita.Identity.Core.Services.Repositories.RoleRepositories;
using Adita.Identity.Core.Services.Repositories.UserClaimRepositories;
using Adita.Identity.Core.Services.Repositories.UserRepositories;
using Adita.Identity.Core.Services.Repositories.UserRoleRepositories;
using Adita.Identity.Core.Test.Services.Repositories.RoleClaimRepositories;
using Adita.Identity.Core.Test.Services.Repositories.RoleRepositories;
using Adita.Identity.Core.Test.Services.Repositories.UserClaimRepositories;
using Adita.Identity.Core.Test.Services.Repositories.UserRepositories;
using Adita.Identity.Core.Test.Services.Repositories.UserRoleRepositories;
using Adita.Security.Principal;
using Adita.Identity.Core.Services.PasswordHashers;
using Adita.Identity.Core.Services.LookupNormalizers;
using Adita.Identity.Core.Services.PasswordValidators;
using Adita.Identity.Core.Services.RoleValidators;
using Adita.Identity.Core.Services.UserValidators;
using Adita.Identity.Core.Services.Factories.ApplicationPrincipalFactories;
using Adita.Identity.Core.Models;
using Adita.Identity.Core.Options;

namespace Adita.Identity.Core.Test.Services.Factories
{
    [TestClass]
    public class ApplicationPrincipleFactoryTest
    {
        private ServiceProvider? serviceProvider;
        private IdentityUser? user;

        [TestInitialize]
        public async Task CanCreate()
        {
            var services = new ServiceCollection();

            services.AddLogging(
                buildier => buildier.AddFileLogger(
                        options => options.Directory = "D://"
                    )
                );

            services.Configure<UserOptions>(
             options =>
             {
                 options.RequireUniqueEmail = false;
                 options.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
             });

            services.Configure<RoleOptions>(
               options =>
               {
                   options.RequiredRoleNameLength = 6;
                   options.AllowedRoleNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
               });

            services.Configure<PasswordOptions>(
              options =>
              {
                  options.RequireDigit = false;
                  options.RequireNonAlphanumeric = false;
              });

            services.Configure<LockoutOptions>(
              options => options.AllowedForNewUsers = false);

            services.Configure<ApplicationIdentityOptions>(
             options =>
             {
                 options.RoleClaimType = ClaimTypes.Role;
             });

            services.AddSingleton<IdentityErrorDescriber>();
            services.AddSingleton<IdentityErrorDescriber>();
            services.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddSingleton<IPasswordHasher<Guid, IdentityUser>, BCryptPasswordHasher<Guid, IdentityUser>>();
            services.AddSingleton<IPasswordValidator, PasswordValidator>();
            services.AddSingleton<IUserValidator<IdentityUser>, UserValidator<Guid, IdentityUser>>();
            services.AddSingleton<IUserRepository<Guid, IdentityUser>, InMemoryUserRepository<Guid, IdentityUser>>();
            services.AddSingleton<IUserClaimRepository<Guid, IdentityUserClaim>, InMemoryUserClaimRepository<Guid, IdentityUserClaim>>();
            services.AddSingleton<IUserRoleRepository<Guid, IdentityUserRole>, InMemoryUserRoleRepository<Guid, IdentityUserRole>>();

            services.AddSingleton<IRoleRepository<Guid, IdentityRole>, InMemoryRoleRepository<Guid, IdentityRole>>();
            services.AddSingleton<IRoleClaimRepository<Guid, IdentityRoleClaim>, InMemoryRoleClaimRepository<Guid, IdentityRoleClaim>>();
            services.AddSingleton<IRoleValidator<IdentityRole>, RoleValidator<Guid, IdentityRole>>();

            services.AddSingleton<IRoleManager<Guid, IdentityRole>, RoleManager<Guid, IdentityRole, IdentityRoleClaim>>();
            services.AddSingleton<IUserManager<Guid, IdentityUser, IdentityRole>, UserManager<Guid, IdentityUser, IdentityUserClaim, IdentityUserRole, IdentityRole>>();

            services.AddSingleton<IApplicationPrincipalFactory<IdentityUser>, ApplicationPrincipalFactory<Guid, IdentityUser, IdentityRole>>();

            serviceProvider = services.BuildServiceProvider();

            IUserManager<Guid, IdentityUser, IdentityRole> userManager =
                serviceProvider.GetRequiredService<IUserManager<Guid, IdentityUser, IdentityRole>>();
            Assert.IsNotNull(userManager);

            IRoleManager<Guid, IdentityRole> roleManager =
                serviceProvider.GetRequiredService<IRoleManager<Guid, IdentityRole>>();
            Assert.IsNotNull(roleManager);

            IdentityResult roleCreateResult = await roleManager.CreateAsync(new IdentityRole("Administrator") { Id = Guid.NewGuid() });
            Assert.IsTrue(roleCreateResult.Succeeded);

            IdentityResult userCreateResult = await userManager.CreateAsync(new IdentityUser("TestUser") { Id = Guid.NewGuid() }, "Password!");
            Assert.IsTrue(userCreateResult.Succeeded);

            user = await userManager.FindByNameAsync("TestUser");
            Assert.IsNotNull(user);

            IdentityRole? role = await roleManager.FindByNameAsync("Administrator");
            Assert.IsNotNull(role);

            IdentityResult addToRoleResult = await userManager.AddToRoleAsync(user, role);
            Assert.IsTrue(addToRoleResult.Succeeded);

            IdentityResult addUserClaimResult = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.MobilePhone, "1234567890"));
            Assert.IsTrue(addUserClaimResult.Succeeded);

            IdentityResult addRoleClaimResult = await roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Uri, "https://test.com"));
            Assert.IsTrue(addRoleClaimResult.Succeeded);
        }

        [TestMethod]
        public async Task VerifyClaims()
        {
            Assert.IsNotNull(serviceProvider);
            Assert.IsNotNull(user);

            IApplicationPrincipalFactory<IdentityUser> principalFactory = serviceProvider.GetRequiredService<IApplicationPrincipalFactory<IdentityUser>>();
            Assert.IsNotNull(principalFactory);

            ApplicationPrincipal principal = await principalFactory.CreateAsync(user);
            Assert.IsNotNull(principal);

            bool isInRole = principal.IsInRole("Administrator");
            Assert.IsTrue(isInRole);

            bool hasPhoneClaim = principal.HasClaim(claim => claim.Type == ClaimTypes.MobilePhone && claim.Value == "1234567890");
            Assert.IsTrue(hasPhoneClaim);

            bool hasUriClaim = principal.HasClaim(claim => claim.Type == ClaimTypes.Uri && claim.Value == "https://test.com");
            Assert.IsTrue(hasUriClaim);
        }

        public void test(UserOptions options)
        {

        }
    }
}
