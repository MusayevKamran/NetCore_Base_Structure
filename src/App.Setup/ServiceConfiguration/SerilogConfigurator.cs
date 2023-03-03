using App.Setup.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

namespace App.Setup.ServiceConfiguration;

/// <summary>
///     Serilog configurator
/// </summary>
public static class SerilogConfigurator
{
    /// <summary>
    ///     Configure Serilog. Register services and settings.
    /// </summary>
    /// <param name="builder">Application Builder</param>
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.SetupLogManager();
        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });
    }

    /// <summary>
    ///     Set up NLog logger manager
    /// </summary>
    /// <param name="builder">Application Builder</param>
    private static void SetupLogManager(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile($"config/api.serilog.{builder.Environment.EnvironmentName.ToLower()}.json", builder.Environment);
    }
    
}