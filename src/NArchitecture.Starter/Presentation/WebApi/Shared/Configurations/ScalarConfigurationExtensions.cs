using Scalar.AspNetCore;

namespace NArchitecture.Starter.WebApi.Shared.Configurations;

/// <summary>
/// Extension methods for configuring Scalar API reference in the application.
/// </summary>
public static class ScalarConfigurationExtensions
{
    /// <summary>
    /// Configures the application to use Scalar API reference.
    /// </summary>
    /// <param name="app">The application to configure.</param>
    public static IApplicationBuilder UseOpenApiScalarUI(this IApplicationBuilder app)
    {
        // Map OpenAPI endpoints
        _ = app.UseRouting();
        _ = app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapOpenApi();
            _ = endpoints.MapScalarApiReference(options =>
            {
                options.Theme = ScalarTheme.Kepler;
                options.CustomCss = """
                .light-mode {
                    --scalar-color-1: #0a2a42;
                    --scalar-color-2: #336699;
                    --scalar-color-3: #4a90e2;
                    --scalar-color-accent: #7fa6d9;
                    --scalar-background-1: #f5faff;
                }
                .dark-mode {
                    --scalar-color-1: #e0f2ff;
                    --scalar-color-2: #a1c4fd;
                    --scalar-color-3: #7fa6d9;
                    --scalar-color-accent: #4a90e2;
                    --scalar-background-1:rgb(0, 11, 18);
                }
                """;
                options.DarkMode = true; // Default theme mode
                options.HideClientButton = true;
            });
        });

        return app;
    }
}
