using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Authentication;
using NArchitecture.Starter.Application.Features.Auth.Models;

namespace NArchitecture.Starter.Application.Features.Auth;

public static class ApplicationAuthFeatureRegistration
{
    public static IServiceCollection AddAuthFeature(
        this IServiceCollection services,
        AdministratorCredentialConfiguration administratorCredentialConfiguration
    )
    {
        _ = services.AddScoped<
            IAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>,
            JwtAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>
        >();

        _ = services.AddSingleton(administratorCredentialConfiguration);

        return services;
    }
}
