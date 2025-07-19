using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Core.Security.Abstractions.Authentication.Entities;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence.Features.Auth.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IRefreshTokenRepository"/>.
/// </summary>
/// <param name="context">The database context.</param>
public class EfRefreshTokenRepository(BaseDbContext context)
    : EfRepositoryBase<RefreshToken, Guid, BaseDbContext>(context),
        IRefreshTokenRepository
{
    /// <inheritdoc/>
    public async Task<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>> AddAsync(
        RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid> refreshTokenEntity,
        CancellationToken cancellationToken
    )
    {
        // Convert generic RefreshToken to concrete implementation if needed
        RefreshToken entity =
            refreshTokenEntity as RefreshToken
            ?? new RefreshToken(
                refreshTokenEntity.UserId,
                refreshTokenEntity.Token,
                refreshTokenEntity.ExpiresAt,
                refreshTokenEntity.CreatedByIp
            )
            {
                Id = refreshTokenEntity.Id,
                RevokedAt = refreshTokenEntity.RevokedAt,
                ReplacedByToken = refreshTokenEntity.ReplacedByToken,
                RevokedByIp = refreshTokenEntity.RevokedByIp,
                ReasonRevoked = refreshTokenEntity.ReasonRevoked,
                CreatedAt = refreshTokenEntity.CreatedAt,
                UpdatedAt = refreshTokenEntity.UpdatedAt,
            };

        RefreshToken addedEntity = await base.AddAsync(entity, cancellationToken);
        return addedEntity;
    }

    /// <inheritdoc/>
    public async Task<ICollection<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>>> GetAllActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        DateTime currentDate = DateTime.UtcNow;

        // Utilize the base GetAll method with a predicate
        var tokens = await base.GetAllAsync(
            predicate: rt => rt.UserId == userId && rt.RevokedAt == null && rt.ExpiresAt > currentDate,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (tokens.Count == 0)
            return Array.Empty<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>>();

        // Cast the collection to the required interface type
        return [.. tokens.Cast<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>>()];
    }

    /// <inheritdoc/>
    public async Task<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token string cannot be null or empty.", nameof(token));

        // Utilize the base GetAsync method
        return await base.GetAsync(
            predicate: rt => rt.Token == token,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
    }

    /// <inheritdoc/>
    public async Task<RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>> UpdateAsync(
        RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid> token,
        CancellationToken cancellationToken
    )
    {
        // Convert generic RefreshToken to concrete implementation if needed
        RefreshToken entity =
            token as RefreshToken
            ?? new RefreshToken(token.UserId, token.Token, token.ExpiresAt, token.CreatedByIp)
            {
                Id = token.Id,
                RevokedAt = token.RevokedAt,
                ReplacedByToken = token.ReplacedByToken,
                RevokedByIp = token.RevokedByIp,
                ReasonRevoked = token.ReasonRevoked,
                CreatedAt = token.CreatedAt,
                UpdatedAt = token.UpdatedAt,
            };

        RefreshToken updatedEntity = await base.UpdateAsync(entity, cancellationToken);
        return updatedEntity;
    }
}
