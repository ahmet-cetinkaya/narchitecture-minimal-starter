using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Mailing.Abstractions;
using NArchitecture.Core.Mailing.MailKit;
using NArchitecture.Starter.Application.Features.Auth.Models;
using NArchitecture.Starter.Application.Features.Auth.Services;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitectureCoreSecurity = NArchitecture.Core.Security;

namespace NArchitecture.Starter.Application.Features.Auth;

public static partial class ApplicationAuthFeatureRegistration
{
    /// <summary>
    /// Registers all authentication and authorization services
    /// </summary>
    public static IServiceCollection AddAuthFeature(this IServiceCollection services, AuthConfiguration configuration)
    {
        return services
            .AddAuthenticationServices(configuration)
            .AddAuthorizationServices()
            .AddAuthenticatorServices(configuration)
            .AddCryptographyServices()
            .AddMailingServices(configuration);
    }

    /// <summary>
    /// Registers JWT authentication services
    /// </summary>
    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, AuthConfiguration configuration)
    {
        _ = services.AddScoped<NArchitectureCoreSecurity.Abstractions.Authentication.IJwtAuthenticationConfiguration>(_ =>
            configuration.JwtAuthenticationConfiguration
        );

        _ = services.AddSingleton<AdministratorCredentialConfiguration>(configuration.AdministratorCredentialConfiguration);

        _ = services.AddScoped<IAuthenticationService, JwtAuthenticationService>();
        _ = services.AddScoped<
            NArchitectureCoreSecurity.Abstractions.Authentication.IAuthenticationService<
                short,
                Guid,
                Guid,
                Guid,
                Guid,
                Guid,
                Guid
            >,
            JwtAuthenticationService
        >();

        return services;
    }

    /// <summary>
    /// Registers authorization services
    /// </summary>
    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IAuthorizationService, JwtAuthorizationService>();
        _ = services.AddScoped<
            NArchitectureCoreSecurity.Abstractions.Authorization.IAuthorizationService<Guid, short>,
            JwtAuthorizationService
        >();

        return services;
    }

    /// <summary>
    /// Registers authenticator services
    /// </summary>
    private static IServiceCollection AddAuthenticatorServices(this IServiceCollection services, AuthConfiguration configuration)
    {
        _ = services.AddScoped<NArchitectureCoreSecurity.Abstractions.Authenticator.IAuthenticatorConfiguration>(_ =>
            configuration.AuthenticatorConfiguration
        );
        _ = services.AddScoped<IAuthenticatorService, AuthenticatorService>();
        _ = services.AddScoped<
            NArchitectureCoreSecurity.Abstractions.Authenticator.IAuthenticatorService<short, Guid, Guid, Guid, Guid, Guid, Guid>,
            AuthenticatorService
        >();

        return services;
    }

    /// <summary>
    /// Registers cryptography services
    /// </summary>
    private static IServiceCollection AddCryptographyServices(this IServiceCollection services)
    {
        _ = services.AddScoped<
            NArchitectureCoreSecurity.Abstractions.Cryptography.Generation.ICodeGenerator,
            NArchitectureCoreSecurity.Cryptography.Generation.CodeGenerator
        >();

        return services;
    }

    /// <summary>
    /// Registers mailing services
    /// </summary>
    private static IServiceCollection AddMailingServices(this IServiceCollection services, AuthConfiguration configuration)
    {
        _ = services.AddSingleton(typeof(Core.Mailing.MailKit.Models.MailConfigration), configuration.MailConfiguration);
        _ = services.AddScoped<IMailService, MailKitMailService>();

        return services;
    }
}
