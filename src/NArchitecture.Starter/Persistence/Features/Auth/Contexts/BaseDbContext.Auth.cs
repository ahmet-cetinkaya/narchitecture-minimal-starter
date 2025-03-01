using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Starter.Application.Features.Auth.Models;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

namespace NArchitecture.Starter.Persistence.Shared.Contexts;

public partial class BaseDbContext : DbContext
{
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAuthenticator> UserAuthenticators { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserGroupOperationClaim> UserGroupOperationClaims { get; set; }
    public DbSet<UserInGroup> UserInGroups { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    private void applyAuthCustomConfigurations(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(
            configuration: new UserConfiguration(
                administratorCredentialConfiguration: serviceProvider.GetRequiredService<AdministratorCredentialConfiguration>()
            )
        );
        _ = modelBuilder.ApplyConfiguration(
            configuration: new UserAuthenticatorConfiguration(
                authenticatorConfiguration: serviceProvider.GetRequiredService<IAuthenticatorConfiguration>()
            )
        );
    }
}
