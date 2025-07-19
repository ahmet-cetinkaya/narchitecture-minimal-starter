using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IAuthorizationService : NArchitectureCoreSecurity.IAuthorizationService<Guid, short>;
