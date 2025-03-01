using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Repositories;

public interface IOperationClaimRepository
    : NArchitectureCoreSecurity.IOperationClaimRepository<short>,
        IAsyncRepository<OperationClaim, short>,
        IRepository<OperationClaim, short>;
