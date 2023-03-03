using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;


namespace App.Setup.ServiceConfiguration;

/// <summary>
///     Service configuration of controlles
/// </summary>
public static class ControllersServiceConfiguration
{
    /// <summary>
    ///     Adds services for controllers to the specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    ///     This method append additional filters for views.
    /// </summary>
    public static void AddControllersWithFilters(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
    }
}