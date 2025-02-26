using NArchitecture.Core.Security.Authorization.Constants;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Seeds;

/// <summary>
/// Provides seed data for OperationClaim entities.
/// </summary>
public static class OperationClaimSeeds
{
    /// <summary>
    /// Predefined claim IDs used for seeding data.
    /// </summary>
    public static class Ids
    {
        public const ushort Admin = 1;
    }

    /// <summary>
    /// Creates seed data for OperationClaim entities.
    /// </summary>
    public static OperationClaim[] CreateSeeds()
    {
        return
        [
            #region OperationClaims (Reserved for NArchitecture.Gen functionality. Don't remove.)
            new(GeneralOperationClaims.Admin) { Id = Ids.Admin, CreatedAt = DateTime.MinValue },
            #endregion
        ];
    }
}
