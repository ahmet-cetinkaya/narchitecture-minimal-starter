using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.EntityFramework.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitecture.Starter.Persistence.Features.Auth.Repositories;
using NArchitecture.Starter.Persistence.Shared.Contexts;
using NArchitecture.Starter.Persistence.Shared.Models;
using NArchitectureCoreSecurityAuthentication = NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitectureCoreSecurityAuthenticator = NArchitecture.Core.Security.Abstractions.Authenticator;

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
        return services.AddDatabase(configuration).AddAuthRepositories();
    }

    /// <summary>
    /// Configures and adds the database context
    /// </summary>
    private static IServiceCollection AddDatabase(this IServiceCollection services, PersistenceConfiguration configuration)
    {
        // Setup for in-memory database
        _ = services.AddDbContext<BaseDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: configuration.Database.Name)
        );

        // Add the database migration applier
        _ = services.AddDbMigrationApplier(sp =>
            sp.GetService<BaseDbContext>() ?? throw new InvalidOperationException("Database context not found.")
        );

        return services;
    }

    /// <summary>
    /// Adds auth-related repositories
    /// </summary>
    private static IServiceCollection AddAuthRepositories(this IServiceCollection services)
    {
        // User repositories
        _ = services.AddScoped<IUserRepository, EfUserRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthentication.IUserRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
            EfUserRepository
        >();

        // Authentication related repositories
        _ = services.AddScoped<IRefreshTokenRepository, EfRefreshTokenRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthentication.IRefreshTokenRepository<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>,
            EfRefreshTokenRepository
        >();

        // Authenticator repositories
        _ = services.AddScoped<IUserAuthenticatorRepository, EfUserAuthenticatorRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthenticator.IUserAuthenticatorRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
            EfUserAuthenticatorRepository
        >();

        // Authorization repositories
        _ = services.AddScoped<IOperationClaimRepository, EfOperationClaimRepository>();
        _ = services.AddScoped<IUserOperationClaimRepository, EfUserOperationClaimRepository>();

        // User group repositories
        _ = services.AddScoped<IUserGroupRepository, EfUserGroupRepository>();
        _ = services.AddScoped<IUserGroupOperationClaimRepository, EfUserGroupOperationClaimRepository>();
        _ = services.AddScoped<IUserInGroupRepository, EfUserInGroupRepository>();

        return services;
    }
}
