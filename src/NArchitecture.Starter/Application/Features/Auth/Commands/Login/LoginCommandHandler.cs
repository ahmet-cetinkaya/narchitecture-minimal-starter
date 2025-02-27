using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Mediator.Abstractions.CQRS;
using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Abstractions.Authentication.Models;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
using NArchitecture.Starter.Application.Features.Auth.Rules;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Application.Features.Auth.Commands.Login;

/// <summary>
/// Handler for processing login commands
/// </summary>
public class LoginCommandHandler(
    IUserRepository userRepository,
    IAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid> authenticationService,
    IAuthenticator<short, Guid, Guid, Guid, Guid, Guid, Guid> authenticator
) : ICommandHandler<LoginCommand, LoggedResponse>
{
    public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(
            predicate: u => u.Email == request.Email,
            include: query => query.Include(u => u.UserAuthenticators!.Where(ua => ua.IsVerified)),
            cancellationToken: cancellationToken
        );
        AuthBusinessRules.UserShouldExist(user);

        LoginRequest<short, Guid, Guid, Guid, Guid, Guid, Guid> loginRequest = new(user!, request.Password, request.IpAddress);
        AuthenticationResponse authResponse = await authenticationService.LoginAsync(loginRequest, cancellationToken);

        UserAuthenticator? verifiedAuthenticator = AuthBusinessRules.GetUserVerifiedAuthenticator(user!);
        // If user has a verified authenticator but no code was provided, require 2FA
        if (verifiedAuthenticator is not null && request.AuthenticatorCode is null)
        {
            string? destination = AuthBusinessRules.GetAuthenticatorDestination(verifiedAuthenticator, user!);
            await authenticator.AttemptAsync(user!.Id, destination, cancellationToken);

            return new LoggedResponse(AccessToken: null, RefreshToken: null, RequiresAuthenticator: true);
        }
        // Verify authenticator code if user has verified authenticator and code is provided
        if (verifiedAuthenticator is not null && request.AuthenticatorCode is not null)
            await authenticator.VerifyAsync(user!.Id, request.AuthenticatorCode, cancellationToken);

        return new LoggedResponse(AccessToken: authResponse.AccessToken, RefreshToken: authResponse.RefreshToken);
    }
}
