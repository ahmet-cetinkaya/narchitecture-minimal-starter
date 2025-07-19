using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authentication.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class User(string email, byte[] passwordSalt, byte[] passwordHash)
    : NArchitectureCoreEntities.User<Guid, short, Guid, Guid, Guid, Guid, Guid>(passwordSalt, passwordHash)
{
    public string Email { get; set; } = email;

    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public User()
        : this(default!, default!, default!) { }

    public virtual ICollection<UserOperationClaim>? UserOperationClaims { get; set; }
    public virtual ICollection<UserInGroup>? UserInGroups { get; set; }
    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    public virtual ICollection<UserAuthenticator>? UserAuthenticators { get; set; }
}
