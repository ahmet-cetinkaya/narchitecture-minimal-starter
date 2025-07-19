using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Features.Auth.Seeds;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the UserOperationClaim entity.
/// </summary>
public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        // Create unique index to prevent duplicate operation claims for a user
        _ = builder
            .HasIndex(uoc => new
            {
                uoc.UserId,
                uoc.OperationClaimId,
                uoc.DeletedAt,
            })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_UserOperationClaim_UserId_ClaimId_Active");

        // Create index for faster lookup of all operation claims for a user
        _ = builder
            .HasIndex(uoc => new { uoc.UserId, uoc.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_UserOperationClaim_UserId_Active");

        // Configure relationships with navigation properties
        _ = builder.HasOne(uoc => uoc.User).WithMany(u => u.UserOperationClaims).HasForeignKey(uoc => uoc.UserId);
        _ = builder
            .HasOne(uoc => uoc.OperationClaim)
            .WithMany(oc => oc.UserOperationClaims)
            .HasForeignKey(uoc => uoc.OperationClaimId);

        // Seed admin user's operation claims
        _ = builder.HasData(UserOperationClaimSeeds.CreateSeeds());

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
