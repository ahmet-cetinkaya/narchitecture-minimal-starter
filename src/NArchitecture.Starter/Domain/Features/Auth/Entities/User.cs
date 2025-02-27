using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authentication.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class User(string email, byte[] passwordSalt, byte[] passwordHash)
    : NArchitectureCoreEntities.User<Guid, short, Guid, Guid, Guid, Guid, Guid>(passwordSalt, passwordHash)
{
    public string Email { get; set; } = email;
}
