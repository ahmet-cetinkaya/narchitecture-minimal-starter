using Microsoft.AspNetCore.Authorization;
using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Core.Security.Authentication;
using NArchitecture.Core.Security.Authenticator;
using NArchitecture.Core.Security.WebApi;
using NArchitecture.Starter.Application.Features.Auth.Models;

namespace NArchitecture.Starter.WebApi.Shared.Configurations;

/// <summary>
/// Contains extension methods for configuring authorization in the application.
/// </summary>
public static class AuthConfigurationExtensions
{
    /// <summary>
    /// Adds authorization services to the service collection.
    /// </summary>
    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.ConfigureJwtAuthentication(configuration.GetJwtConfiguration());

        _ = services.AddAuthorization(options =>
        {
            // Add authorization with default policy
            options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        });

        return services;
    }

    /// <summary>
    /// Configures the application to use authentication and authorization middleware.
    /// </summary>
    public static IApplicationBuilder UseAuthorizationServices(this IApplicationBuilder app)
    {
        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        return app;
    }

    /// <summary>
    /// Gets the JWT authentication configuration from application settings.
    /// </summary>
    public static IJwtAuthenticationConfiguration GetJwtConfiguration(this IConfiguration configuration)
    {
        // Get JWT configuration from application settings
        IConfigurationSection jwtSection = configuration.GetSection("Auth:JWT");
        string securityKey =
            jwtSection["SecurityKey"] ?? throw new InvalidOperationException("Auth:JWT:SecurityKey is not configured.");
        string issuer = jwtSection["Issuer"] ?? throw new InvalidOperationException("Auth:JWT:Issuer is not configured.");
        string audience = jwtSection["Audience"] ?? throw new InvalidOperationException("Auth:JWT:Audience is not configured.");
        if (
            !int.TryParse(jwtSection["AccessTokenExpiration"], out int accessTokenExpirationMinutes)
            || accessTokenExpirationMinutes <= 0
        )
            throw new InvalidOperationException("Auth:JWT:AccessTokenExpiration must be a positive integer.");

        // Create JWT authentication configuration
        DefaultJwtAuthenticationConfiguration jwtConfiguration = new(
            securityKey,
            issuer,
            audience,
            TimeSpan.FromMinutes(accessTokenExpirationMinutes)
        );

        return jwtConfiguration;
    }

    public static AdministratorCredentialConfiguration GetAdministratorCredentialConfiguration(this IConfiguration configuration)
    {
        AdministratorCredentialConfiguration administratorCredentialConfiguration =
            configuration.GetSection("Auth:Administrator").Get<AdministratorCredentialConfiguration>()
            ?? throw new InvalidOperationException("Auth:Administrator configuration is not found.");

        return administratorCredentialConfiguration;
    }

    public static IAuthenticatorConfiguration GetAuthenticatorConfiguration(this IConfiguration configuration)
    {
        DefaultAuthenticatorConfiguration authenticatorConfiguration =
            configuration.GetSection("Auth:Authenticator").Get<DefaultAuthenticatorConfiguration>()
            ?? new DefaultAuthenticatorConfiguration();

        return authenticatorConfiguration;
    }
}
