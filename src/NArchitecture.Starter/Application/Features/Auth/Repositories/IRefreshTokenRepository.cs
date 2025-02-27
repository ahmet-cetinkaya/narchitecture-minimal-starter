using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authentication;

namespace NArchitecture.Starter.Application.Features.Auth.Repositories;

public interface IRefreshTokenRepository
    : NArchitectureCoreSecurity.IRefreshTokenRepository<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<RefreshToken, Guid>,
        IRepository<RefreshToken, Guid>;
