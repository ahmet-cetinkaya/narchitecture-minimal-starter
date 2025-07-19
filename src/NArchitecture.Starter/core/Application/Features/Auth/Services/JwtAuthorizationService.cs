using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Services;

public class JwtAuthorizationService(IUserRepository userRepository)
    : NArchitectureCoreSecurity.JwtAuthorizationService<short, Guid, Guid, Guid, Guid, Guid, Guid>(userRepository),
        IAuthorizationService;
