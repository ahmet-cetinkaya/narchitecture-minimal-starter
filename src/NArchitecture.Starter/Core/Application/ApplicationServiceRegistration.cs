using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.DependencyInjection;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;
using NArchitecture.Core.Mediator;
using NArchitecture.Core.Validation.FluentValidation.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth;
using NArchitecture.Starter.Application.Features.Auth.Models;

namespace NArchitecture.Starter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, AuthConfiguration authConfiguration)
    {
        // Get the current assembly that contains our handlers
        Assembly currentAssembly = typeof(ApplicationServiceRegistration).Assembly;

        // Register NArchitecture.Core.Mediator.Abstractions.IMediator service with explicit assembly scanning
        _ = services.AddMediator(currentAssembly);

        // Register NArchitecture.Core.Application.IBusinessRules services
        _ = services.AddBusinessRules();

        // Register NArchitecture.Core.Validation.Abstractions.IValidator<T> services for all FluentValidation.IValidator<T> services
        _ = services.AddFluentValidation(assemblies: [currentAssembly]);

        // Register NArchitecture.Core.Mapping.Abstractions.IMapper services for all AutoMapper.Profile services
        _ = services.AddAutoMapper(currentAssembly);

        // Register NArchitecture.Core.Localization.Abstractions.ILocalizationService service with YamlResourceLocalization
        _ = services.AddYamlResourceLocalization();

        // Register features
        _ = services.AddAuthFeature(authConfiguration);

        return services;
    }
}
