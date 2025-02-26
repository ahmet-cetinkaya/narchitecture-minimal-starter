using Microsoft.EntityFrameworkCore;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

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
}
