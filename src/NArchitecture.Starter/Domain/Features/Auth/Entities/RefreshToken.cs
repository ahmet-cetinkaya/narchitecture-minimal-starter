using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authentication.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class RefreshToken(Guid userId, string token, DateTime expiresAt, string createdByIp)
    : NArchitectureCoreEntities.RefreshToken<Guid, Guid, Guid>(userId, token, expiresAt, createdByIp) { }
