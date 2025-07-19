using NArchitecture.Starter.WebApi.Features.Auth.Endpoints;

namespace NArchitecture.Starter.WebApi.Features.Auth;

/// <summary>
/// Configures all authentication-related endpoints
/// </summary>
public static class AuthEndpointRegistration
{
    /// <summary>
    /// Creates and configures the Auth route group
    /// </summary>
    public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/auth").WithTags("Auth");

        _ = group.MapLoginEndpoint();

        return group;
    }
}
