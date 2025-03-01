using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Mailing.Abstractions;
using NArchitecture.Core.Mailing.MailKit;
using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Core.Security.Abstractions.Authorization;
using NArchitecture.Core.Security.Abstractions.Cryptography.Generation;
using NArchitecture.Core.Security.Authentication;
using NArchitecture.Core.Security.Authenticator;
using NArchitecture.Core.Security.Authorization;
using NArchitecture.Core.Security.Cryptography.Generation;
using NArchitecture.Starter.Application.Features.Auth.Models;

namespace NArchitecture.Starter.Application.Features.Auth;

public static partial class ApplicationAuthFeatureRegistration
{
    public static IServiceCollection AddAuthFeature(this IServiceCollection services, AuthConfiguration configuration)
    {
        AddCoreServices(services, configuration);

        return services;
    }

    private static void AddCoreServices(IServiceCollection services, AuthConfiguration configuration)
    {
        // Add authentication services
        _ = services.AddScoped<IJwtAuthenticationConfiguration>(_ => configuration.JwtAuthenticationConfiguration);
        _ = services.AddSingleton<AdministratorCredentialConfiguration>(configuration.AdministratorCredentialConfiguration);
        _ = services.AddScoped<
            IAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>,
            JwtAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>
        >();

        // Add cryptography services
        _ = services.AddScoped<ICodeGenerator, CodeGenerator>();

        // Add authorization services
        _ = services.AddScoped<
            IAuthorizationService<Guid, short>,
            JwtAuthorizationService<short, Guid, Guid, Guid, Guid, Guid, Guid>
        >();
        _ = services.AddScoped(_ => configuration.AuthenticatorConfiguration);
        _ = services.AddScoped<
            IAuthenticator<short, Guid, Guid, Guid, Guid, Guid, Guid>,
            Authenticator<short, Guid, Guid, Guid, Guid, Guid, Guid>
        >();

        // Add mailing services
        _ = services.AddSingleton(typeof(Core.Mailing.MailKit.Models.MailConfigration), configuration.MailConfiguration);
        _ = services.AddScoped<IMailService, MailKitMailService>();
    }
}
