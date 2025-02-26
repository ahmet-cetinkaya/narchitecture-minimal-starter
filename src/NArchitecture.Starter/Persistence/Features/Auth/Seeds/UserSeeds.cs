using System.Security.Cryptography;
using System.Text;
using NArchitecture.Starter.Application.Features.Auth.Constants;
using NArchitecture.Starter.Domain.Features.Auth.Entities;

namespace NArchitecture.Starter.Persistence.Features.Auth.Seeds;

/// <summary>
/// Provides seed data for User entities.
/// </summary>
public static class UserSeeds
{
    /// <summary>
    /// Predefined entity IDs used for seeding data.
    /// </summary>
    public static class Ids
    {
        public static readonly Guid Admin = Guid.Parse("00000000-0000-0000-0000-000000000001");
    }

    /// <summary>
    /// Creates seed data for User entities.
    /// </summary>
    /// <param name="configuration">Administrator credentials configuration.</param>
    public static User[] CreateSeeds(AdministratorCredentialConfiguration configuration)
    {
        // Generate password hash and salt for admin user
        using HMACSHA512 hmac = new();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(configuration.Password));

        return [new User(configuration.Email, passwordSalt, passwordHash) { Id = Ids.Admin, CreatedAt = DateTime.MinValue }];
    }
}
