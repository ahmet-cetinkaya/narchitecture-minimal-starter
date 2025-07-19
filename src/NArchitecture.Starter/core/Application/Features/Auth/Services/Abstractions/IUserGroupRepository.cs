using NArchitecture.Core.Persistence.Abstractions.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Abstractions.Authorization;

namespace NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;

public interface IUserGroupRepository
    : NArchitectureCoreSecurity.IUserGroupRepository<Guid, short, Guid, Guid, Guid, Guid, Guid>,
        IAsyncRepository<UserGroup, Guid>,
        IRepository<UserGroup, Guid>;
