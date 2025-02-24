using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid> { }
