using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserGroup(string name) : NArchitectureCoreEntities.UserGroup<Guid, ushort, Guid, Guid, Guid, Guid, Guid>(name);
