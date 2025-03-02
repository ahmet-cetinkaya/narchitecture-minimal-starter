using NArchitecture.Core.Persistence.EntityFramework.Repositories;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitecture.Starter.Persistence.Shared.Contexts;

namespace NArchitecture.Starter.Persistence.Features.Auth.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IUserGroupOperationClaimRepository"/>.
/// </summary>
/// <param name="context">The database context.</param>
public class EfUserGroupOperationClaimRepository(BaseDbContext context)
    : EfRepositoryBase<UserGroupOperationClaim, Guid, BaseDbContext>(context),
        IUserGroupOperationClaimRepository;
