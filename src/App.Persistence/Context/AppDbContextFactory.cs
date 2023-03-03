using App.Persistence.Context.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Context;

/// <summary>
///     Factory database context
/// </summary>
public class AppDbContextFactory : IAppDbContextFactory
{
    /// <summary>
    ///  Database options
    /// </summary>
    private readonly DbContextOptions _options;

    public AppDbContextFactory(DbContextOptions options)
    {
        _options = options;
    }

    /// <summary>
    ///     Create new Database context
    /// </summary>
    /// <returns>New Database context</returns>
    public AppDbContext CreateContext()
    {
        return new AppDbContext(_options);
    }
}