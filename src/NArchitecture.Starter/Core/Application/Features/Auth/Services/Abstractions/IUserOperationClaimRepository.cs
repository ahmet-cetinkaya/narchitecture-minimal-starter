using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IUserOperationClaimRepository
    : NArchitectureCoreSecurity.IUserOperationClaimRepository<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<UserOperationClaim, Guid>,
        IRepository<UserOperationClaim, Guid>;
