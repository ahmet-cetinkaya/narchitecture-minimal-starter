using NArchitecture.Core.Mailing.MailKit.Models;
using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Abstractions.Authenticator;

namespace NArchitecture.Starter.Application.Features.Auth.Models;

public record struct AuthConfiguration(
    AdministratorCredentialConfiguration AdministratorCredentialConfiguration,
    IJwtAuthenticationConfiguration JwtAuthenticationConfiguration,
    IAuthenticatorConfiguration AuthenticatorConfiguration,
    MailConfigration MailConfiguration
);
