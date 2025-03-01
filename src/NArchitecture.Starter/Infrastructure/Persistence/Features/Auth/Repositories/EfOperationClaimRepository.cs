using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Starter.Application.Features.Auth.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence.Features.Auth.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IOperationClaimRepository"/>.
/// </summary>
/// <param name="context">The database context.</param>
public class EfOperationClaimRepository(BaseDbContext context)
    : EfRepositoryBase<OperationClaim, short, BaseDbContext>(context),
        IOperationClaimRepository;
