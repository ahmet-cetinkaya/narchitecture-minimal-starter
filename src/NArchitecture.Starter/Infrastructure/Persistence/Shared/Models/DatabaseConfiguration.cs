namespace NArchitecture.Starter.Persistence.Shared.Models;

/// <summary>
/// Configuration for database services
/// </summary>
/// <param name="Name">The name of the database</param>
public readonly record struct DatabaseConfiguration(string Name);
