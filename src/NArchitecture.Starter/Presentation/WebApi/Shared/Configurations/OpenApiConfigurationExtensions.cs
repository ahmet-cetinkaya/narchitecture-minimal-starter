using NArchitecture.Starter.WebApi.Shared.Models;

namespace NArchitecture.Starter.WebApi.Shared.Configurations;

/// <summary>
/// Extension methods for configuring OpenAPI in the application.
/// </summary>
public static class OpenApiConfigurationExtensions
{
    /// <summary>
    /// Adds OpenAPI services to the service collection based on provided configuration.
    /// </summary>
    /// <param name="services">The service collection to add OpenAPI services to.</param>
    /// <param name="configuration">The application configuration.</param>
    public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        OpenApiConfiguration openApiOptions = configuration.GetOpenApiConfiguration();

        _ = services.AddOpenApi(options =>
        {
            _ = options.AddDocumentTransformer(
                (document, context, cancellationToken) =>
                {
                    document.Info = new()
                    {
                        Title = openApiOptions.Title,
                        Version = openApiOptions.Version,
                        Description = openApiOptions.Description,
                        Contact = new() { Name = openApiOptions.Contact.Name, Email = openApiOptions.Contact.Email },
                        License = new() { Name = openApiOptions.License.Name, Url = new Uri(openApiOptions.License.Url) },
                        TermsOfService = new Uri(openApiOptions.TermsOfServiceUrl),
                    };
                    return Task.CompletedTask;
                }
            );
        });

        return services;
    }

    /// <summary>
    /// Gets the OpenAPI configuration from the application configuration.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public static OpenApiConfiguration GetOpenApiConfiguration(this IConfiguration configuration)
    {
        OpenApiConfiguration openApiOptions = configuration.GetSection("OpenApi").Get<OpenApiConfiguration>();
        return openApiOptions;
    }
}
