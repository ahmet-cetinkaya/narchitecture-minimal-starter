using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserGroupOperationClaim(Guid userGroupId, ushort operationClaimId)
    : NArchitectureCoreEntities.UserGroupOperationClaim<Guid, ushort, Guid, Guid, Guid, Guid, Guid>(
        userGroupId,
        operationClaimId
    );
