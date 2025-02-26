namespace NArchitecture.Starter.Persistence;

/// <summary>
/// Represents the configuration for the persistence layer.
/// </summary>
/// <param name="DatabaseName">The name of the database.</param>
public readonly record struct PersistenceConfiguration(string DatabaseName);
