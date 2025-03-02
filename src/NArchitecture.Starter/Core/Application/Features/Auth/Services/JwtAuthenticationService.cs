using NArchitecture.Core.Security.Abstractions.Authentication;
using NArchitecture.Core.Security.Abstractions.Authorization;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Authentication;

namespace NArchitecture.Starter.Application.Features.Auth.Services;

public class JwtAuthenticationService(
    IRefreshTokenRepository<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid> refreshTokenRepository,
    IUserRepository<Guid, short, Guid, Guid, Guid, Guid, Guid> userRepository,
    IAuthorizationService<Guid, short> authorizationService,
    IJwtAuthenticationConfiguration configuration
)
    : NArchitectureCoreSecurity.JwtAuthenticationService<short, Guid, Guid, Guid, Guid, Guid, Guid>(
        refreshTokenRepository,
        userRepository,
        authorizationService,
        configuration
    ),
        IAuthenticationService { }
