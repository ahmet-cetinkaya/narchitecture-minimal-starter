using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Application.Features.Auth.Constants;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Features.Auth.Seeds;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the User entity.
/// </summary>
public class UserConfiguration(AdministratorCredentialConfiguration administratorCredentialConfiguration)
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configure property constraints
        const int MaxEmailLength = 256;
        const int MaxHashLength = 512;
        const int MaxSaltLength = 512;

        _ = builder.Property(u => u.Email).HasMaxLength(MaxEmailLength).IsRequired();
        _ = builder.Property(u => u.PasswordHash).HasMaxLength(MaxHashLength).IsRequired();
        _ = builder.Property(u => u.PasswordSalt).HasMaxLength(MaxSaltLength).IsRequired();

        // Create unique index for email on active users
        _ = builder
            .HasIndex(u => new { u.Email, u.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_User_Email_Active");

        // Configure relationships with proper cascade delete
        _ = builder.HasMany(u => u.UserOperationClaims).WithOne().HasForeignKey(uoc => uoc.UserId);
        _ = builder.HasMany(u => u.RefreshTokens).WithOne().HasForeignKey(rt => rt.UserId);
        _ = builder.HasMany(u => u.UserAuthenticators).WithOne().HasForeignKey(ua => ua.UserId);

        // Seed user data
        _ = builder.HasData(UserSeeds.CreateSeeds(administratorCredentialConfiguration));

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
