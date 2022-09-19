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
using Adita.Identity.Core.Services;
using Adita.Identity.EntityFrameworkCore.Models.DbContexts;

namespace Adita.Identity.EntityFrameworkCore.Services.Repositories.RoleRepositories
{
    /// <summary>
    /// Represents a base class for a role persistence repository.
    /// </summary>
    /// <typeparam name="TKey">The type used for the primary key of a role.</typeparam>
    public class RoleRepository<TKey> :
        RoleRepository<TKey, IdentityRole<TKey>, IdentityDbContext<TKey>>
        where TKey : IEquatable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initialize a new instance of <see cref="RoleRepository{TKey}" /> using specified
        /// <paramref name="context" /> and <paramref name="errorDescriber" />.
        /// </summary>
        /// <param name="context">A <see cref="IdentityDbContext{TKey}"/> to retrieve the users from.</param>
        /// <param name="errorDescriber">An <see cref="IdentityErrorDescriber" />
        /// to get localized error strings from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context" /> or <paramref name="errorDescriber" /> is <c>null</c></exception>
        public RoleRepository(IdentityDbContext<TKey> context, IdentityErrorDescriber errorDescriber)
            : base(context, errorDescriber)
        {
        }
        #endregion Constructors
    }
}
