using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.EntityFramework.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
using NArchitecture.Starter.Persistence.Features.Auth.Repositories;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence;

public static class PersistenceServiceRegistration
{
    /// <summary>
    /// Adds the persistence services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The persistence configuration.</param>
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        PersistenceConfiguration configuration
    )
    {
        // Add the database context
        _ = services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase(configuration.DatabaseName));

        // Add the database migration applier
        _ = services.AddDbMigrationApplier(sp =>
            sp.GetService<BaseDbContext>() ?? throw new InvalidOperationException("Database context not found.")
        );

        // Add the repositories
        _ = services.AddScoped<IUserRepository, EfUserRepository>();
        _ = services.AddScoped<IUserAuthenticatorRepository, EfUserAuthenticatorRepository>();
        _ = services.AddScoped<IOperationClaimRepository, EfOperationClaimRepository>();
        _ = services.AddScoped<IRefreshTokenRepository, EfRefreshTokenRepository>();
        _ = services.AddScoped<IUserGroupRepository, EfUserGroupRepository>();
        _ = services.AddScoped<IUserGroupOperationClaimRepository, EfUserGroupOperationClaimRepository>();
        _ = services.AddScoped<IUserInGroupRepository, EfUserInGroupRepository>();
        _ = services.AddScoped<IUserOperationClaimRepository, EfUserOperationClaimRepository>();

        return services;
    }
}
