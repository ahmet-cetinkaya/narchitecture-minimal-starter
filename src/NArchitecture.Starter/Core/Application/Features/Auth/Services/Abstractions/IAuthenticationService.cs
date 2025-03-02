using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authentication;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IAuthenticationService
    : NArchitectureCoreSecurity.IAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>;
