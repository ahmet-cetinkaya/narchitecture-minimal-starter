using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authentication;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

/// <summary>
/// Repository interface for user operations
/// </summary>
public interface IUserRepository
    : NArchitectureCoreSecurity.IUserRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<User, Guid>,
        IRepository<User, Guid>;
