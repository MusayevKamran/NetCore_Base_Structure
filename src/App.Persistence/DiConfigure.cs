using App.Persistence.Context;
using App.Persistence.Context.Interfaces;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using App.Persistence.Settings;

namespace App.Persistence;

/// <summary>
///     Dependency injection
/// </summary>
public static class DiConfigure
{
    /// <summary>
    ///     Add persistence layer in dependency injection container
    /// </summary>
    /// <param name="services">Collection services</param>
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddSettings<IConnectionSetting, AppDatabaseSettings>();
        AddDbContext(services);
    }

    /// <summary>
    ///     Create scope for Database
    /// </summary>
    private static void AddDbContext(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var settings = serviceProvider.GetRequiredService<IConnectionSetting>();
        
        var optionsBuilder = new DbContextOptionsBuilder();
        if (settings.ConnectionString != null)
        {
            optionsBuilder.UseSqlServer(settings.ConnectionString);
            
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(settings.ConnectionString);
            });
        }

        services.AddSingleton<IAppDbContextFactory>(sp => new AppDbContextFactory(optionsBuilder.Options));
    }
}