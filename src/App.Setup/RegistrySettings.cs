using App.Setup.AppSettings;
using App.Setup.AppSettings.Interfaces;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace App.Setup;

/// <summary>
///     Registry of settings
/// </summary>
public static class RegistrySettings
{
    /// <summary>
    ///     Register app settings by sections
    /// </summary>
    public static void RegisterSettings(this IServiceCollection services)
    {
        services.AddSettings<ISwaggerSettings, SwaggerSettings>();
    }
}