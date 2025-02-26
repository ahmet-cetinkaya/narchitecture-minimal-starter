using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserOperationClaim(Guid userId, ushort operationClaimId)
    : NArchitectureCoreEntities.UserOperationClaim<Guid, ushort, Guid, Guid, Guid, Guid, Guid, Guid>(userId, operationClaimId);
