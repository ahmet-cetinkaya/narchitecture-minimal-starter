using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the UserInGroup entity.
/// </summary>
public class UserInGroupConfiguration : IEntityTypeConfiguration<UserInGroup>
{
    public void Configure(EntityTypeBuilder<UserInGroup> builder)
    {
        // Create unique index to prevent duplicate group memberships for a user
        _ = builder
            .HasIndex(uig => new
            {
                uig.UserId,
                uig.GroupId,
                uig.DeletedAt,
            })
            .HasFilter("\"DeletedAt\" IS NULL")
            .IsUnique()
            .HasDatabaseName("IX_UserInGroup_UserId_GroupId_Active");

        // Create index for faster lookup of all groups for a user
        _ = builder
            .HasIndex(uig => new { uig.UserId, uig.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_UserInGroup_UserId_Active");

        // Create index for faster lookup of all users in a group
        _ = builder
            .HasIndex(uig => new { uig.GroupId, uig.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_UserInGroup_GroupId_Active");

        // Configure relationships with navigation properties
        _ = builder.HasOne<User>().WithMany().HasForeignKey(uig => uig.UserId);
        _ = builder.HasOne<UserGroup>().WithMany().HasForeignKey(uig => uig.GroupId);

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
