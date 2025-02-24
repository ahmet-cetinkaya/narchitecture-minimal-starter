using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim, int>, IRepository<OperationClaim, int> { }
