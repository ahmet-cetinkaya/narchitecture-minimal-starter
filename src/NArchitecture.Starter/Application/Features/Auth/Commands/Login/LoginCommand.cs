using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Mediator.Abstractions.CQRS;

namespace NArchitecture.Starter.Application.Features.Auth.Commands.Login;

/// <summary>
/// Command to authenticate a user with email and password
/// </summary>
/// <param name="Email">User's email address</param>
/// <param name="Password">User's password</param>
/// <param name="AuthenticatorCode">Optional authenticator code for two-factor authentication</param>
/// <param name="IpAddress">Client IP address for security logging</param>
public record LoginCommand(string Email, string Password, string? AuthenticatorCode, string IpAddress)
    : ICommand<LoggedResponse>,
        ILoggableRequest,
        ITransactionalRequest;
