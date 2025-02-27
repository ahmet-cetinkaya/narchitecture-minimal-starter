using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserGroupOperationClaim(Guid userGroupId, short operationClaimId)
    : NArchitectureCoreEntities.UserGroupOperationClaim<Guid, short, Guid, Guid, Guid, Guid, Guid>(
        userGroupId,
        operationClaimId
    );
