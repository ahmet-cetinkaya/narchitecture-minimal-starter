using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.DependencyInjection;
using NArchitecture.Core.Mediator;
using NArchitecture.Core.Validation.FluentValidation.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth;

namespace NArchitecture.Starter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register NArchitecture.Core.Mediator service
        _ = services.AddMediator();

        // Register NArchitecture.Core.Mapping.Abstractions.IMapper services for all AutoMapper.Profile services
        _ = services.AddAutoMapper(typeof(ApplicationServiceRegistration).Assembly);

        // Register NArchitecture.Core.Validation.Abstractions.IValidator<T> services for all FluentValidation.IValidator<T> services
        _ = services.AddFluentValidation(assemblies: [typeof(ApplicationServiceRegistration).Assembly]);

        // Register NArchitecture.Core.Application.IBusinessRules services
        _ = services.AddBusinessRules();

        // Register features
        _ = services.AddAuthFeature();

        return services;
    }
}
