//MIT License

//Copyright (c) 2022 Adita

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Adita.Identity.Core.Models;
using System.Security.Claims;

namespace Adita.Identity.Core.Services.Managers.UserManagers
{
    /// <summary>
    /// Provides an abstraction for managing user in a persistence repository.
    /// </summary>
    /// <typeparam name="TKey">The type used for the primary key of the user and role.</typeparam>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    public interface IUserManager<TKey, TUser, TRole> : IDisposable
        where TKey : IEquatable<TKey>
        where TUser : class
        where TRole : class
    {
        #region Methods
        /// <summary>
        /// Increments the access failed count for the user as an asynchronous operation.
        /// If the failed access account is greater than or equal to the configured maximum number of attempts,
        /// the user will be locked out for the configured lockout time span.
        /// </summary>
        /// <param name="user">The user whose failed access count to increment.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AccessFailedAsync(TUser user);
        /// <summary>
        /// Adds the specified <paramref name="claim"/> to the <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AddClaimAsync(TUser user, Claim claim);
        /// <summary>
        /// Adds the specified <paramref name="claims"/> to the <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AddClaimsAsync(TUser user, IEnumerable<Claim> claims);
        /// <summary>
        /// Adds the <paramref name="password"/> to the specified <paramref name="user"/> only if the user does not already have a password.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="password">The password to set.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AddPasswordAsync(TUser user, string password);
        /// <summary>
        /// Add the specified <paramref name="user"/> to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="role">The name of the role to add the user to.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AddToRoleAsync(TUser user, TRole role);
        /// <summary>
        /// Add the specified <paramref name="user"/> to the named roles.
        /// </summary>
        /// <param name="user">The user to add to the named roles.</param>
        /// <param name="roles">The name of the roles to add the user to.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AddToRolesAsync(TUser user, IEnumerable<TRole> roles);
        /// <summary>
        /// Changes a user's password after confirming the specified <paramref name="currentPassword"/> is correct, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="currentPassword">The current password to validate before changing.</param>
        /// <param name="newPassword">The new password to set for the specified <paramref name="user"/>.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword);
        /// <summary>
        /// Returns a flag indicating whether the given <paramref name="password"/> is valid for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose password should be validated.</param>
        /// <param name="password">The password to validate</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing true if the specified <paramref name="password"/> matches the one store for the <paramref name="user"/>, otherwise false.</returns>
        Task<bool> CheckPasswordAsync(TUser user, string password);
        /// <summary>
        /// Creates the specified <paramref name="user"/> in the backing store with given <paramref name="password"/>, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user to hash and store.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> CreateAsync(TUser user, string password);
        /// <summary>
        /// Deletes the specified <paramref name="user"/> from the backing store.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> DeleteAsync(TUser user);
        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId"/> if it exists.</returns>
        Task<TUser?> FindByIdAsync(TKey userId);
        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userName"/> if it exists.</returns>
        Task<TUser?> FindByNameAsync(string userName);
        /// <summary>
        /// Retrieves the current number of failed accesses for the given <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose access failed count should be retrieved for.</param>
        /// <returns>The <see cref="Task"/> that contains the result the asynchronous operation, the current failed access count for the user.</returns>
        Task<int> GetAccessFailedCountAsync(TUser user);
        /// <summary>
        /// Gets a list of claims to be belonging to the specified <paramref name="user"/> as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose claims to retrieve.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of claims.</returns>
        Task<IList<Claim>> GetClaimsAsync(TUser user);
        /// <summary>
        /// Retrieves a flag indicating whether user lockout can be enabled for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, true if a user can be locked out, otherwise false.</returns>
        Task<bool> GetLockoutEnabledAsync(TUser user);
        /// <summary>
        /// Gets the last <see cref="DateTimeOffset"/> a user's last lockout expired, if any. A time value in the past indicates a user is not currently locked out.
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the lookup, a <see cref="DateTimeOffset"/> containing the last time a user's lockout expired, if any.</returns>
        Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user);
        /// <summary>
        /// Gets a list of roles the specified <paramref name="user"/> belongs to.
        /// </summary>
        /// <param name="user">The user whose roles to retrieve.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing a list of roles.</returns>
        Task<IList<TRole>> GetRolesAsync(TUser user);
        /// <summary>
        /// Gets the user identifier for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user"/>.</returns>
        Task<TKey> GetUserIdAsync(TUser user);
        /// <summary>
        /// Gets the user name for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the name for the specified <paramref name="user"/>.</returns>
        Task<string> GetUserNameAsync(TUser user);
        /// <summary>
        /// Returns a list of users from the user store who have the specified <paramref name="claim"/>.
        /// </summary>
        /// <param name="claim">The claim to look for.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of users who have the specified claim.</returns>
        Task<IList<TUser>> GetUsersForClaimAsync(Claim claim);
        /// <summary>
        /// Returns a list of users from the user store who are members of the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The role whose users should be returned.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of users who are members of the specified role.</returns>
        Task<IList<TUser>> GetUsersInRoleAsync(TRole role);
        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user"/> has a password.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user"/> has a password otherwise false.</returns>
        Task<bool> HasPasswordAsync(TUser user);
        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user"/> is a member of the given role.
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="role">The role to be checked.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user"/> is a member of the role.</returns>
        Task<bool> IsInRoleAsync(TUser user, TRole role);
        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user"/> is locked out, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose locked out status should be retrieved.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, true if the specified <paramref name="user"/> is locked out, otherwise false.</returns>
        Task<bool> IsLockedOutAsync(TUser user);
        /// <summary>
        /// Returns whether specified <paramref name="user"/> is exist on repository.
        /// </summary>
        /// <param name="user">A user to check the existence.</param>
        /// <returns>A <see cref="Task"/> that represents as asynchronous operation which contain whether the specified <paramref name="user"/> is exist.</returns>
        Task<bool> IsUserExistAsync(TUser user);
        /// <summary>
        /// Normalize user or role name for consistent comparisons.
        /// </summary>
        /// <param name="name">The name to normalize.</param>
        /// <returns>A normalized value representing the specified <paramref name="name"/>.</returns>
        string NormalizeName(string name);
        /// <summary>
        /// Removes the specified <paramref name="claim"/> from the given <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user to remove the specified <paramref name="claim"/> from.</param>
        /// <param name="claim">The <see cref="Claim"/> to remove.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> RemoveClaimAsync(TUser user, Claim claim);
        /// <summary>
        /// Removes the specified <paramref name="claims"/> from the given <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user to remove the specified <paramref name="claims"/> from.</param>
        /// <param name="claims">A collection of <see cref="Claim"/>s to remove.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims);
        /// <summary>
        /// Removes the specified <paramref name="user"/> from the named role.
        /// </summary>
        /// <param name="user">The user to remove from the named role.</param>
        /// <param name="role">The name of the role to remove the user from.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> RemoveFromRoleAsync(TUser user, TRole role);
        /// <summary>
        /// Removes the specified <paramref name="user"/> from the named roles.
        /// </summary>
        /// <param name="user">The user to remove from the named <paramref name="roles"/>.</param>
        /// <param name="roles">The name of the roles to remove the <paramref name="user"/> from.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveFromRolesAsync(TUser user, IEnumerable<TRole> roles);
        /// <summary>
        /// emoves a user's password.
        /// </summary>
        /// <param name="user">The user whose password should be removed.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> RemovePasswordAsync(TUser user);
        /// <summary>
        /// Resets the access failed count for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose failed access count should be reset.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> ResetAccessFailedCountAsync(TUser user);
        /// <summary>
        /// Resets the <paramref name="user"/>'s password to the specified <paramref name="newPassword"/>.
        /// </summary>
        /// <param name="user">The user whose password should be reset.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(TUser user, string newPassword);
        /// <summary>
        /// Sets a flag indicating whether the specified <paramref name="user"/> is locked out, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose locked out status should be set.</param>
        /// <param name="enabled">Flag indicating whether the user is locked out or not.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, the <see cref="IdentityResult"/> of the operation</returns>
        Task<IdentityResult> SetLockoutEnabledAsync(TUser user, bool enabled);
        /// <summary>
        /// Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a user.
        /// </summary>
        /// <param name="user">The user whose lockout date should be set.</param>
        /// <param name="lockoutEnd">The <see cref="DateTimeOffset"/> after which the <paramref name="user"/>'s lockout should end.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd);
        /// <summary>
        /// Sets the given <paramref name="userName"/> for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task<IdentityResult> SetUserNameAsync(TUser user, string userName);
        /// <summary>
        /// Updates the specified <paramref name="user"/> in the backing store.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> UpdateAsync(TUser user);
        /// <summary>
        /// Updates a user's password hash.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="validatePassword">Whether to validate the password.</param>
        /// <returns>Whether the password has was successfully updated.</returns>
        Task<IdentityResult> UpdatePasswordHash(TUser user, string newPassword, bool validatePassword);
        /// <summary>
        /// Should return <see cref="IdentityResult.Success"/> if validation is successful. This is called before updating the password hash.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="IdentityResult"/> representing whether validation was successful.</returns>
        Task<IdentityResult> ValidatePasswordAsync(TUser user, string password);
        /// <summary>
        /// Should return <see cref="IdentityResult.Success"/> if validation is successful. This is called before saving the user via Create or Update.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>A <see cref="IdentityResult"/> representing whether validation was successful.</returns>
        Task<IdentityResult> ValidateUserAsync(TUser user);
        /// <summary>
        /// Returns a <see cref="PasswordVerificationResult"/> indicating the result of a password hash comparison.
        /// </summary>
        /// <param name="user">The user whose password should be verified.</param>
        /// <param name="password">The password to verify.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="PasswordVerificationResult"/> of the operation.</returns>
        Task<PasswordVerificationResult> VerifyPasswordAsync(TUser user, string password);
        #endregion Methods
    }
}
