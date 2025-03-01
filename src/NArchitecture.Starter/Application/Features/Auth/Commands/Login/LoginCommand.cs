using NArchitecture.Core.Mediator.Abstractions.CQRS;

namespace NArchitecture.Starter.Application.Features.Auth.Commands.Login;

/// <summary>
/// Command for user login operations
/// </summary>
/// <param name="Email">User's email address</param>
/// <param name="Password">User's password</param>
/// <param name="AuthenticatorCode">Optional authenticator code for 2FA</param>
/// <param name="IpAddress">Client IP address</param>
public record LoginCommand(string Email, string Password, string? AuthenticatorCode, string IpAddress) : ICommand<LoggedResponse>;
