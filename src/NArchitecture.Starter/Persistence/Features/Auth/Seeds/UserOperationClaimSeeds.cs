using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Seeds;

/// <summary>
/// Provides seed data for UserOperationClaim entities.
/// </summary>
public static class UserOperationClaimSeeds
{
    /// <summary>
    /// Creates seed data for UserOperationClaim entities based on administrator configuration.
    /// </summary>
    public static UserOperationClaim[] CreateSeeds()
    {
        return
        [
            new(UserSeeds.Ids.Admin, OperationClaimSeeds.Ids.Admin)
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedAt = DateTime.UtcNow,
            },
        ];
    }
}
