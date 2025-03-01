using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Starter.Application.Features.Auth.Constants;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Application.Features.Auth.Rules;

public partial class AuthBusinessRules
{
    /// <summary>
    /// Verifies that a user exists, throwing an exception if not found
    /// </summary>
    /// <param name="user">The user to check</param>
    /// <exception cref="BusinessException">Thrown when user is null</exception>
    public static void UserShouldExist(User? user)
    {
        if (user is null)
            throw new BusinessException(AuthMessages.User.NotFound);
    }
}
