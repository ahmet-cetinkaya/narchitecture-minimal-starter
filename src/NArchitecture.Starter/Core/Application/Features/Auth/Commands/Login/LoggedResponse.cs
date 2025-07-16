using NArchitecture.Core.Security.Abstractions.Authentication.Models;

namespace NArchitecture.Starter.Application.Features.Auth.Commands.Login;

/// <summary>
/// Response returned after a successful login
/// </summary>
/// <param name="AccessToken">JWT access token for API authorization</param>
/// <param name="RefreshToken">Refresh token for obtaining a new access token</param>
/// <param name="RequiresAuthenticator">Indicates whether two-factor authentication is required</param>
public record LoggedResponse(Token? AccessToken = null, Token? RefreshToken = null, bool RequiresAuthenticator = false);
