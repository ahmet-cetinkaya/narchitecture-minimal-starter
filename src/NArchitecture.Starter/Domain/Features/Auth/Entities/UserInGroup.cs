using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserInGroup(Guid userId, Guid userGroupId)
    : NArchitectureCoreEntities.UserInGroup<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>(userId, userGroupId);
