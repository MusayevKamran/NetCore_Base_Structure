using System.Diagnostics.CodeAnalysis;
using App.Setup.AppSettings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace App.Setup.AppSettings;

/// <summary>
///     Configuration section of swagger
/// </summary>
[ExcludeFromCodeCoverage]
internal class SwaggerSettings : ISwaggerSettings
{
    public SwaggerSettings(IConfiguration configuration)
    {
        XmlComments = configuration.GetSection("Swagger:XmlComments").Get<string[]>();
    }

    /// <summary>
    ///     XML comments for swagger
    /// </summary>
    public string[]? XmlComments { get; set; }
}