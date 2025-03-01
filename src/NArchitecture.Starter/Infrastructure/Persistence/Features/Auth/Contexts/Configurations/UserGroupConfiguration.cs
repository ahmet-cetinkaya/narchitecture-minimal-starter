using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the UserGroup entity.
/// </summary>
public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        // Configure property constraints
        const int MaxNameLength = 50;
        _ = builder.Property(ug => ug.Name).HasMaxLength(MaxNameLength).IsRequired();

        // Create unique index for name on active groups
        _ = builder
            .HasIndex(ug => new { ug.Name, ug.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_UserGroup_Name_Active");

        // Configure relationships
        _ = builder.HasMany<UserInGroup>().WithOne().HasForeignKey(uig => uig.GroupId);
        _ = builder.HasMany<UserGroupOperationClaim>().WithOne().HasForeignKey(ugoc => ugoc.UserGroupId);

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
