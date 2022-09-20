namespace Adita.Identity.Core.Models
{
    /// <summary>
    /// Determines the sign-in result of a user.
    /// </summary>
    public enum SignInResult
    {
        /// <summary>
        /// Indicates that the sign-in result is succeeded.
        /// </summary>
        Succeeded,
        /// <summary>
        /// Indicates that the sign-in result is failed.
        /// </summary>
        Failed,
        /// <summary>
        /// Indicates that the sign-in result has invalid credential.
        /// </summary>
        InvalidCredential,
        /// <summary>
        /// Indicates that the user who are trying to sign-in is locked out.
        /// </summary>
        LockedOut,
        /// <summary>
        /// Indicates that the user who are trying to sign-in is not allowed to sign-in.
        /// </summary>
        NotAllowed
    }
}
