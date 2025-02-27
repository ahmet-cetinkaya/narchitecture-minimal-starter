using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserOperationClaim(Guid userId, short operationClaimId)
    : NArchitectureCoreEntities.UserOperationClaim<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>(userId, operationClaimId);
