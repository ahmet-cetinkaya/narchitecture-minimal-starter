using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authentication.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class RefreshToken(Guid userId, string token, DateTime expiresAt, string createdByIp)
    : NArchitectureCoreEntities.RefreshToken<Guid, short, Guid, Guid, Guid, Guid, Guid>(userId, token, expiresAt, createdByIp)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public RefreshToken()
        : this(default, default!, default, default!) { }
}
