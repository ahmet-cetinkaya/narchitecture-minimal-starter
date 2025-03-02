using NArchitecture.Core.Security.Abstractions.Authenticator.Enums;
using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authenticator.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserAuthenticator(Guid userId, AuthenticatorType type)
    : NArchitectureCoreEntities.UserAuthenticator<Guid, short, Guid, Guid, Guid, Guid, Guid>(userId, type)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public UserAuthenticator()
        : this(default, default) { }

    public virtual User? User { get; set; }
}
