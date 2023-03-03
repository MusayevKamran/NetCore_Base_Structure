using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace App.Setup.ServiceConfiguration;

public static class SwaggerConfiguration
{
    /// <summary>
    ///     Create scope for Swagger
    /// </summary>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }

    /// <summary>
    ///     Use swagger configuration UI
    /// </summary>
    public static void UseSwagger(this WebApplication app)
    {
        SwaggerBuilderExtensions.UseSwagger(app);
        app.UseSwaggerUI();
    }
}