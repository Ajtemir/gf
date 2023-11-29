using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAccountService
{
    /// <summary>
    /// Sign in the current user and set authorization cookie.
    /// </summary>
    /// <param name="userName">Unique userName.</param>
    /// <param name="password">User password.</param>
    /// <param name="cancellationToken">Cancellation token used to cancel the operation.</param>
    /// <exception cref="BadRequestException">Invalid credentials.</exception>
    /// <returns>The current user data.</returns>
    Task<ApplicationUser> Login(string userName, string password, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sign out the current user and remove the authorization cookie.
    /// </summary>
    Task Logout();
}