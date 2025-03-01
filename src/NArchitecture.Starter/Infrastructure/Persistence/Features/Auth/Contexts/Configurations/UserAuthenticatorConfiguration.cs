using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the UserAuthenticator entity.
/// </summary>
/// <param name="authenticatorConfiguration">The authenticator configuration containing security parameters.</param>
public class UserAuthenticatorConfiguration(IAuthenticatorConfiguration authenticatorConfiguration)
    : IEntityTypeConfiguration<UserAuthenticator>
{
    public void Configure(EntityTypeBuilder<UserAuthenticator> builder)
    {
        // Configure property constraints
        _ = builder.Property(ua => ua.CodeSeed).HasMaxLength(authenticatorConfiguration.CodeSeedLength);
        _ = builder.Property(ua => ua.Code).HasMaxLength(authenticatorConfiguration.CodeLength);

        // Composite index for active authenticators by user and type
        _ = builder
            .HasIndex(ua => new
            {
                ua.UserId,
                ua.Type,
                ua.DeletedAt,
            })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_UserAuthenticator_UserId_Type_Active");

        // Configure the relationship with User
        _ = builder.HasOne<User>().WithMany().HasForeignKey(ua => ua.UserId);

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
