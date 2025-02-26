using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Core.Security.Abstractions.Authentication.Entities;
using NArchitecture.Core.Security.Abstractions.Authorization.Entities;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence.Features.Auth.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IUserRepository"/>.
/// </summary>
/// <param name="context">The database context.</param>
public class EfUserRepository(BaseDbContext context) : EfRepositoryBase<User, Guid, BaseDbContext>(context), IUserRepository
{
    /// <inheritdoc/>
    public async Task<User<Guid, ushort, Guid, Guid, Guid, Guid, Guid>?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var user = await base.GetByIdAsync(id: userId, enableTracking: false, cancellationToken: cancellationToken);
        return user;
    }

    /// <inheritdoc/>
    public async Task<ICollection<OperationClaim<ushort>>> GetOperationClaimsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        // Get user with all related claims in a single query
        var user = await base.GetByIdAsync(
            id: id,
            include: query =>
                query
                    .Include(u => u.UserOperationClaims!)
                    .ThenInclude(uoc => uoc.OperationClaim!)
                    .Include(u => u.UserInGroups!)
                    .ThenInclude(uig => uig.UserGroup!)
                    .ThenInclude(ug => ug.UserGroupOperationClaims!)
                    .ThenInclude(ugoc => ugoc.OperationClaim!),
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (user == null)
            return Array.Empty<OperationClaim<ushort>>();

        // Get user's direct operation claims
        List<OperationClaim<ushort>> userOperationClaims = user.UserOperationClaims!.Select(uoc => uoc.OperationClaim).ToList()!;

        // Get user's group operation claims
        List<OperationClaim<ushort>> userGroupOperationClaims = user.UserInGroups!.SelectMany(uig =>
                uig.UserGroup!.UserGroupOperationClaims!.Select(ugoc => ugoc.OperationClaim)
            )
            .ToList()!;

        // Combine and remove duplicates
        List<OperationClaim<ushort>> allOperationClaims = [.. userOperationClaims.Union(userGroupOperationClaims).Distinct()];

        return allOperationClaims;
    }

    /// <inheritdoc/>
    public async Task<bool> HasAllPermissionsAsync(
        Guid id,
        IEnumerable<string> permissionNames,
        CancellationToken cancellationToken = default
    )
    {
        var permissionsList = permissionNames.ToList();
        if (permissionsList.Count == 0)
            return true;

        // Get all claim names in a single query
        var allClaimNames = await GetUserClaimNamesAsync(id, cancellationToken);

        // Check if all requested permissions match the claim names
        return permissionsList.All(allClaimNames.Contains);
    }

    /// <inheritdoc/>
    public async Task<bool> HasAnyPermissionAsync(
        Guid id,
        IEnumerable<string> permissionNames,
        CancellationToken cancellationToken = default
    )
    {
        var permissionsList = permissionNames.ToList();
        if (permissionsList.Count == 0)
            return false;

        // Check if user exists
        bool userExists = await base.ExistsByIdAsync(id, cancellationToken: cancellationToken);
        if (!userExists)
            return false;

        // Get all claim names in a single query
        var allClaimNames = await GetUserClaimNamesAsync(id, cancellationToken);

        // Check if any requested permissions match the claim names
        return permissionsList.Any(allClaimNames.Contains);
    }

    /// <inheritdoc/>
    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(permissionName))
            throw new ArgumentException("Permission name cannot be null or empty.", nameof(permissionName));

        // Check if user exists
        bool userExists = await base.ExistsByIdAsync(userId, cancellationToken: cancellationToken);
        if (!userExists)
            return false;

        // Use base repository methods to check for permission
        bool hasDirectPermission = await base.AnyAsync(
            predicate: u => u.Id == userId && u.UserOperationClaims!.Any(uoc => uoc.OperationClaim!.Name == permissionName),
            cancellationToken: cancellationToken
        );

        if (hasDirectPermission)
            return true;

        // Check group permissions
        bool hasGroupPermission = await base.AnyAsync(
            predicate: u =>
                u.Id == userId
                && u.UserInGroups!.Any(uig =>
                    uig.UserGroup!.UserGroupOperationClaims!.Any(ugoc => ugoc.OperationClaim!.Name == permissionName)
                ),
            cancellationToken: cancellationToken
        );

        return hasGroupPermission;
    }

    // Helper method to get all claim names for a user using base repository methods
    private async Task<HashSet<string>> GetUserClaimNamesAsync(Guid userId, CancellationToken cancellationToken)
    {
        // Get user with claims in a single query
        var user = await base.GetByIdAsync(
            id: userId,
            include: query =>
                query
                    .Include(u => u.UserOperationClaims!)
                    .ThenInclude(uoc => uoc.OperationClaim!)
                    .Include(u => u.UserInGroups!)
                    .ThenInclude(uig => uig.UserGroup!)
                    .ThenInclude(ug => ug.UserGroupOperationClaims!)
                    .ThenInclude(ugoc => ugoc.OperationClaim!),
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (user == null)
            return [];

        // Collect direct claim names
        var directClaimNames = user.UserOperationClaims!.Select(uoc => uoc.OperationClaim!.Name).ToList();

        // Collect group claim names
        var groupClaimNames = user.UserInGroups!.SelectMany(uig =>
                uig.UserGroup!.UserGroupOperationClaims!.Select(ugoc => ugoc.OperationClaim!.Name)
            )
            .ToList();

        // Combine all claim names in a HashSet for efficient lookups
        var allClaimNames = new HashSet<string>(directClaimNames);
        allClaimNames.UnionWith(groupClaimNames);

        return allClaimNames;
    }
}
