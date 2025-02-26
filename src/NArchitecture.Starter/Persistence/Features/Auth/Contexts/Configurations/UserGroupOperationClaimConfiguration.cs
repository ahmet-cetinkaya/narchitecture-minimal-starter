using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the UserGroupOperationClaim entity.
/// </summary>
public class UserGroupOperationClaimConfiguration : IEntityTypeConfiguration<UserGroupOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserGroupOperationClaim> builder)
    {
        // Create unique index to prevent duplicate operation claims for a user group
        _ = builder
            .HasIndex(ugoc => new
            {
                ugoc.UserGroupId,
                ugoc.OperationClaimId,
                ugoc.DeletedAt,
            })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_UserGroupOperationClaim_GroupId_ClaimId_Active");

        // Create index for faster lookup of all operation claims for a user group
        _ = builder
            .HasIndex(ugoc => new { ugoc.UserGroupId, ugoc.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_UserGroupOperationClaim_GroupId_Active");

        // Configure relationships
        _ = builder.HasOne<UserGroup>().WithMany().HasForeignKey(ugoc => ugoc.UserGroupId);
        _ = builder.HasOne<OperationClaim>().WithMany().HasForeignKey(ugoc => ugoc.OperationClaimId);

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
