using Adita.Identity.Core.Models;

namespace Adita.Identity.Core.Services.Managers.SignInManagers
{
    /// <summary>
    /// Provides an abstraction for sign in manager.
    /// </summary>
    /// <typeparam name="TUser">The type used for the user.</typeparam>
    public interface ISignInManager<TUser>
        where TUser : class
    {
        #region Methods
        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/> combination asynchronously.
        /// </summary>
        /// <param name="userName">The user name to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <returns>A <see cref="Task"/> that represents an asynchronous operation which contains a <see cref="SignInResult"/>.</returns>
        public Task<SignInResult> PasswordSignInAsync(string userName, string password);
        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents an asynchronous operation.</returns>
        public Task SignOutAsync();
        #endregion Methods
    }
}
