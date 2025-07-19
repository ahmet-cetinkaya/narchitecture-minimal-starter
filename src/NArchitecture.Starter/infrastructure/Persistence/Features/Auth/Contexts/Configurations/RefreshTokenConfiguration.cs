using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Contexts.Configurations;

/// <summary>
/// Configures the entity mappings and database schema for the RefreshToken entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Configure property constraints
        const byte MaxTokenLength = 64; // Token is a Base64 string generated from a 32-byte array (44-48 chars)
        const byte MaxIpLength = 45; // IP address fields - maximum length to accommodate IPv6 addresses

        _ = builder.Property(rt => rt.Token).HasMaxLength(MaxTokenLength).IsRequired();
        _ = builder.Property(rt => rt.CreatedByIp).HasMaxLength(MaxIpLength).IsRequired();
        _ = builder.Property(rt => rt.RevokedByIp).HasMaxLength(MaxIpLength);
        _ = builder.Property(rt => rt.ReplacedByToken).HasMaxLength(MaxTokenLength);
        _ = builder.Property(rt => rt.ReasonRevoked).HasMaxLength(255);

        // Index for token lookups, filtered to only include non-deleted tokens
        _ = builder
            .HasIndex(rt => new { rt.Token, rt.DeletedAt })
            .HasFilter("\"DeletedAt\" IS NULL")
            .HasDatabaseName("IX_RefreshToken_Token_Active");

        // Configure the relationship with User
        _ = builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId);

        // Ignore base type
        _ = builder.HasBaseType((string)null!);
    }
}
