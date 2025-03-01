using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Repositories;

public interface IUserGroupOperationClaimRepository
    : NArchitectureCoreSecurity.IUserGroupOperationClaimRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<UserGroupOperationClaim, Guid>,
        IRepository<UserGroupOperationClaim, Guid>;
