using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace Application.Services.Repositories;

public interface IOtpAuthenticatorRepository : IAsyncRepository<OtpAuthenticator, Guid>, IRepository<OtpAuthenticator, Guid> { }
