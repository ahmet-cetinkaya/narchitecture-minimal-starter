using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Core.Security.Abstractions.Authentication.Entities;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
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
    public async Task<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>> AddAsync(
        RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid> refreshTokenEntity,
        CancellationToken cancellationToken
    )
    {
        RefreshToken entity =
            refreshTokenEntity as RefreshToken
            ?? throw new ArgumentException(
                "Cannot process the given refresh token type. Expected concrete RefreshToken implementation.",
                nameof(refreshTokenEntity)
            );

        RefreshToken addedEntity = await base.AddAsync(entity, cancellationToken);
        return addedEntity;
    }

    /// <inheritdoc/>
    public async Task<ICollection<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>>> GetAllActiveByUserIdAsync(
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
            return Array.Empty<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>>();

        // Cast the collection to the required interface type
        return [.. tokens.Cast<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>>()];
    }

    /// <inheritdoc/>
    public async Task<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>?> GetByTokenAsync(
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
    public async Task<RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid>> UpdateAsync(
        RefreshToken<Guid, ushort, Guid, Guid, Guid, Guid, Guid> token,
        CancellationToken cancellationToken
    )
    {
        RefreshToken entity =
            token as RefreshToken
            ?? throw new ArgumentException(
                "Cannot process the given refresh token type. Expected concrete RefreshToken implementation.",
                nameof(token)
            );

        RefreshToken updatedEntity = await base.UpdateAsync(entity, cancellationToken);
        return updatedEntity;
    }
}
