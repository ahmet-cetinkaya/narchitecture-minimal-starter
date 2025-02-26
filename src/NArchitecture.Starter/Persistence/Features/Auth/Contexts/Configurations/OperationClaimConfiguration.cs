using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Features.Auth.Seeds;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the OperationClaim entity.
/// </summary>
public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        // Configure property constraints
        _ = builder.Property(oc => oc.Name).HasMaxLength(255).IsRequired();

        // Create a unique index on Name for non-deleted claims
        _ = builder
            .HasIndex(oc => new { oc.Name, oc.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_OperationClaim_Name_Active");

        // Seed initial data
        _ = builder.HasData(OperationClaimSeeds.CreateSeeds());

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
