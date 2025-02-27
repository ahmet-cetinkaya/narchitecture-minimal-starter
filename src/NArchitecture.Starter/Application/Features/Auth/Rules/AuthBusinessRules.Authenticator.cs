using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Security.Abstractions.Authenticator.Enums;
using NArchitecture.Starter.Application.Features.Auth.Constants;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Application.Features.Auth.Rules;

public partial class AuthBusinessRules
{
    /// <summary>
    /// Verifies that a user has authenticators and gets the first verified one
    /// </summary>
    public static UserAuthenticator? GetUserVerifiedAuthenticator(User user)
    {
        if (user.UserAuthenticators is null)
            throw new BusinessException(AuthMessages.Authenticator.NotFound);

        return (UserAuthenticator?)user.UserAuthenticators.FirstOrDefault(ua => ua.IsVerified);
    }

    /// <summary>
    /// Validates that the user has authenticators
    /// </summary>
    /// <param name="user">The user to validate</param>
    /// <exception cref="BusinessException">Thrown when user authenticators collection is null</exception>
    public static void UserAuthenticatorsShouldExist(User user)
    {
        if (user.UserAuthenticators is null)
            throw new BusinessException(AuthMessages.Authenticator.NotFound);
    }

    /// <summary>
    /// Gets the destination for an authenticator based on its type
    /// </summary>
    /// <param name="authenticator">The authenticator</param>
    /// <param name="user">The user</param>
    /// <returns>Destination string (e.g., email address) or null if not applicable</returns>
    public static string? GetAuthenticatorDestination(UserAuthenticator authenticator, User user)
    {
        return authenticator.Type switch
        {
            AuthenticatorType.Email => user.Email,
            _ => null,
        };
    }
}
