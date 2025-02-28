namespace NArchitecture.Starter.Application.Features.Auth.Models;

/// <summary>
/// Configuration for administrator credentials.
/// </summary>
/// <param name="Email">The email address.</param>
/// <param name="Password">The password.</param>
public record AdministratorCredentialConfiguration(string Email, string Password);
