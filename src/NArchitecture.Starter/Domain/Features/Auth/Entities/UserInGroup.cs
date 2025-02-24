using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserInGroup(Guid userId, ushort groupId)
    : NArchitectureCoreEntities.UserInGroup<Guid, Guid, Guid, ushort>(userId, groupId) { }
