using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.EntityFramework.DependencyInjection;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
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
    /// <param name="databaseConfiguration">The persistence configuration.</param>
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        PersistenceConfiguration configuration
    )
    {
        // Setup for in-memory database
        _ = services.AddDbContext<BaseDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: configuration.Database.Name)
        );

        // Add the database migration applier
        _ = services.AddDbMigrationApplier(sp =>
            sp.GetService<BaseDbContext>() ?? throw new InvalidOperationException("Database context not found.")
        );

        // Add the repositories
        _ = services.AddScoped<IUserRepository, EfUserRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthentication.IUserRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
            EfUserRepository
        >();

        _ = services.AddScoped<IUserAuthenticatorRepository, EfUserAuthenticatorRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthenticator.IUserAuthenticatorRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
            EfUserAuthenticatorRepository
        >();

        _ = services.AddScoped<IOperationClaimRepository, EfOperationClaimRepository>();

        _ = services.AddScoped<IRefreshTokenRepository, EfRefreshTokenRepository>();
        _ = services.AddScoped<
            NArchitectureCoreSecurityAuthentication.IRefreshTokenRepository<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>,
            EfRefreshTokenRepository
        >();

        _ = services.AddScoped<IUserGroupRepository, EfUserGroupRepository>();
        _ = services.AddScoped<IUserGroupOperationClaimRepository, EfUserGroupOperationClaimRepository>();
        _ = services.AddScoped<IUserInGroupRepository, EfUserInGroupRepository>();
        _ = services.AddScoped<IUserOperationClaimRepository, EfUserOperationClaimRepository>();

        return services;
    }
}
