using NArchitecture.Starter.Application;
using NArchitecture.Starter.Application.Features.Auth.Models;
using NArchitecture.Starter.Persistence;
using NArchitecture.Starter.Persistence.Shared.Models;

namespace NArchitecture.Starter.WebApi.Shared.Configurations;

/// <summary>
/// Contains extension methods for registering layer services to the service collection.
/// </summary>
public static class LayerServiceRegistrationExtensions
{
    /// <summary>
    /// Registers application layer services with their configurations.
    /// </summary>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        AuthConfiguration authConfiguration = new(
            AdministratorCredentialConfiguration: configuration.GetAdministratorCredentialConfiguration(),
            JwtAuthenticationConfiguration: configuration.GetJwtConfiguration(),
            AuthenticatorConfiguration: configuration.GetAuthenticatorConfiguration(),
            MailConfiguration: configuration.GetMailConfiguration()
        );
        _ = services.AddApplicationServices(authConfiguration);

        return services;
    }

    /// <summary>
    /// Registers persistence layer services with their configurations.
    /// </summary>
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        PersistenceConfiguration persistenceConfiguration = new(Database: configuration.GetDatabaseConfiguration());
        _ = services.AddPersistenceServices(persistenceConfiguration);

        return services;
    }
}
