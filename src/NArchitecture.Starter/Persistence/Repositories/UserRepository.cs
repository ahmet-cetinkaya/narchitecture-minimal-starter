using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, Guid, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
