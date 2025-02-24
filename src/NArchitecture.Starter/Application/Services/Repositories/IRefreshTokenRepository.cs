using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, Guid>, IRepository<RefreshToken, Guid>
{
    Task<List<RefreshToken>> GetOldRefreshTokensAsync(Guid userId, int refreshTokenTTL);
}
