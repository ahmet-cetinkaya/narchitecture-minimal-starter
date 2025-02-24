using NArchitecture.Core.Security.Abstractions.Authenticator.Enums;
using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authenticator.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserAuthenticator(Guid userId, AuthenticatorType type)
    : NArchitectureCoreEntities.UserAuthenticator<Guid, Guid>(userId, type) { }
