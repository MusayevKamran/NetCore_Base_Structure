namespace App.Persistence.Context.Interfaces;

/// <summary>
///     Contract for database context factory
/// </summary>
public interface IAppDbContextFactory
{
    /// <summary>
    ///     Create new database context
    /// </summary>
    AppDbContext CreateContext();
}