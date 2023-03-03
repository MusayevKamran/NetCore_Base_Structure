using App.Setup.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace App.Setup.ServiceConfiguration;

/// <summary>
///     Config files and environments configurations
/// </summary>
public static class ConfigFileConfiguration
{
    /// <summary>
    ///     Adds the JSON configuration and EnvironmentVariables
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddConfigs(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();
        builder.SetEnvironment();
        builder.Configuration.AddJsonFile("config/api.appsettings.json",
            $"config/api.env.{builder.Environment.EnvironmentName.ToLower()}.json", builder.Environment);
    }
}