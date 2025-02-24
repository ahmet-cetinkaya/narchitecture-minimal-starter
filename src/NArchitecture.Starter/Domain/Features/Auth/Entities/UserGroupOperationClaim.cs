using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserGroupOperationClaim(ushort userGroupId, ushort operationClaimId)
    : NArchitectureCoreEntities.UserGroupOperationClaim<int, ushort, Guid, Guid, Guid, ushort>(userGroupId, operationClaimId) { }
