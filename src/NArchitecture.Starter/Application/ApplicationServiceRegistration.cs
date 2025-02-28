using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.DependencyInjection;
using NArchitecture.Core.Localization.Abstractions;
using NArchitecture.Core.Localization.Resource.Yaml;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;
using NArchitecture.Core.Mediator;
using NArchitecture.Core.Validation.FluentValidation.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth;
using NArchitecture.Starter.Application.Features.Auth.Models;

namespace NArchitecture.Starter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        AdministratorCredentialConfiguration administratorCredentialConfiguration
    )
    {
        // Register NArchitecture.Core.Mediator.Abstractions.IMediator service
        _ = services.AddMediator();

        // Register NArchitecture.Core.Application.IBusinessRules services
        _ = services.AddBusinessRules();

        // Register NArchitecture.Core.Validation.Abstractions.IValidator<T> services for all FluentValidation.IValidator<T> services
        _ = services.AddFluentValidation(assemblies: [typeof(ApplicationServiceRegistration).Assembly]);

        // Register NArchitecture.Core.Mapping.Abstractions.IMapper services for all AutoMapper.Profile services
        _ = services.AddAutoMapper(typeof(ApplicationServiceRegistration).Assembly);

        // Register  NArchitecture.Core.Localization.Abstractions.ILocalizationService service with YamlResourceLocalization
        _ = services.AddYamlResourceLocalization();

        // Register features
        _ = services.AddAuthFeature(administratorCredentialConfiguration);

        return services;
    }
}
