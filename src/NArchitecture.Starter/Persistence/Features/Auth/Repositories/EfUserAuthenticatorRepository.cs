using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Core.Security.Abstractions.Authenticator.Entities;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence.Features.Auth.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IUserAuthenticatorRepository"/>.
/// </summary>
/// <param name="context">The database context.</param>
public class EfUserAuthenticatorRepository(BaseDbContext context)
    : EfRepositoryBase<UserAuthenticator, Guid, BaseDbContext>(context),
        IUserAuthenticatorRepository
{
    /// <inheritdoc/>
    public async Task<UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid>> AddAsync(
        UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid> authenticator, 
        CancellationToken cancellationToken)
    {
        if (authenticator == null)
            throw new ArgumentNullException(nameof(authenticator), "User authenticator cannot be null");
        
        UserAuthenticator entity = authenticator as UserAuthenticator 
            ?? throw new ArgumentException("Cannot process the given authenticator type. Expected concrete UserAuthenticator implementation.", nameof(authenticator));

        // Use the base AddAsync method
        var addedEntity = await base.AddAsync(entity, cancellationToken);
        return addedEntity;
    }

    /// <inheritdoc/>
    public async Task<UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid>> DeleteAsync(
        UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid> authenticator, 
        CancellationToken cancellationToken)
    {
        if (authenticator == null)
            throw new ArgumentNullException(nameof(authenticator), "User authenticator cannot be null");
        
        UserAuthenticator entity = authenticator as UserAuthenticator 
            ?? throw new ArgumentException("Cannot process the given authenticator type. Expected concrete UserAuthenticator implementation.", nameof(authenticator));

        // Use the base DeleteAsync method with permanent deletion
        var deletedEntity = await base.DeleteAsync(entity, true, cancellationToken);
        return deletedEntity;
    }

    /// <inheritdoc/>
    public async Task<UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid>?> GetByIdAsync(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        // Use the base GetAsync method with a predicate for UserId
        return await base.GetAsync(
            predicate: a => a.UserId == userId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
    }

    /// <inheritdoc/>
    public async Task<UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid>> UpdateAsync(
        UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid> authenticator, 
        CancellationToken cancellationToken)
    {
        if (authenticator == null)
            throw new ArgumentNullException(nameof(authenticator), "User authenticator cannot be null");
        
        UserAuthenticator entity = authenticator as UserAuthenticator 
            ?? throw new ArgumentException("Cannot process the given authenticator type. Expected concrete UserAuthenticator implementation.", nameof(authenticator));

        // Use the base UpdateAsync method
        var updatedEntity = await base.UpdateAsync(entity, cancellationToken);
        return updatedEntity;
    }
}
