using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authenticator;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IAuthenticatorService
    : NArchitectureCoreSecurity.IAuthenticatorService<short, Guid, Guid, Guid, Guid, Guid, Guid>;
