using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class OperationClaim(string name) : NArchitectureCoreEntities.OperationClaim<ushort>(name);
