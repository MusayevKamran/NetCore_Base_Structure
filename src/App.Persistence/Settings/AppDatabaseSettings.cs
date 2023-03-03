using System.Diagnostics.CodeAnalysis;
using bgTeam.DataAccess;
using Microsoft.Extensions.Configuration;

namespace App.Persistence.Settings;

/// <summary>
///     Implementation database settings
/// </summary>
[ExcludeFromCodeCoverage]
internal class AppDatabaseSettings : IConnectionSetting
{
    public AppDatabaseSettings(IConfiguration configuration)
    {
        ConnectionString = configuration["Database:ConnectionString"];
    }

    /// <summary>
    ///     Database connection string
    /// </summary>

    public string ConnectionString { get; set; }
}