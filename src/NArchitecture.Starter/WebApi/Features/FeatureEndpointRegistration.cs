using NArchitecture.Starter.WebApi.Features.Auth;

namespace NArchitecture.Starter.WebApi.Features;

/// <summary>
/// Configures all feature endpoints
/// </summary>
public static class FeaturesEndpointRegistration
{
    /// <summary>
    /// Maps all feature endpoints
    /// </summary>
    public static WebApplication MapFeaturesEndpoints(this WebApplication app)
    {
        // Map feature endpoints
        _ = app.MapAuthEndpoints();

        return app;
    }
}
