using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authenticator;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IUserAuthenticatorRepository
    : NArchitectureCoreSecurity.IUserAuthenticatorRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<UserAuthenticator, Guid>,
        IRepository<UserAuthenticator, Guid>;
