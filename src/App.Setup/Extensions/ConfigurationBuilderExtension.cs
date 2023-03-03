using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace App.Setup.Extensions;

/// <summary>
///     Extension of configuration builder
/// </summary>
public static class ConfigurationBuilderExtension
{
    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" />.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this IConfigurationBuilder configuration, string configFile, IHostEnvironment environment)
    {
        var filePath = GetJsonFile(configFile, environment);
        if (!File.Exists(filePath))
            return;

        var configs = File.ReadAllText(filePath);
        configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(configs)));
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from <paramref name="envFile" />.
    ///     If the environment file does not exist, then environment variables will be applied.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this IConfigurationBuilder configuration, string configFile, string envFile, IHostEnvironment environment)
    {
        if (File.Exists(GetJsonFile(envFile, environment)))
            configuration.AddJsonFileWithEnvironmentFile(configFile, envFile, environment);
        else
            configuration.AddJsonFileWithEnvironmentVariables(configFile, environment);
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from <paramref name="envFile" />.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    private static void AddJsonFileWithEnvironmentFile(this IConfigurationBuilder configuration, string configFile, string envFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var environmentFilePath = GetJsonFile(envFile, environment);

        var configs = File.ReadAllText(configFilePath);
        var envData = File.ReadAllText(environmentFilePath);
        var environments = JsonConvert.DeserializeObject<IDictionary<string, string>>(envData);

        var config = environments?.Aggregate(configs, (current, env) => current.Replace($"${env.Key}", env.Value));
        if (config != null)
            configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(config)));
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from EnvironmentsVariables.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    private static void AddJsonFileWithEnvironmentVariables(this IConfigurationBuilder configuration, string configFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var configs = File.ReadAllText(configFilePath);

        configs = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>().Aggregate(configs, (current, env) => current.Replace($"${env.Key}", env.Value?.ToString()));

        configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(configs)));
    }

    /// <summary>
    ///     Setup environment from <paramref name="appEnv" /> from Kubernates ConfigMap.
    /// </summary>
    /// <param name="builder">A builder for web application and services</param>
    /// <param name="appEnv">APP_ENV</param>
    public static void SetEnvironment(this WebApplicationBuilder builder, string appEnv = "APP_ENV")
    {
        var value = Environment.GetEnvironmentVariable(appEnv);
        if (string.IsNullOrEmpty(value))
            return;

        builder.Environment.EnvironmentName = value;
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", value);
    }

    /// <summary>
    ///     Get JSON file
    /// </summary>
    /// <param name="pathFile">File path</param>
    /// <param name="environment">Host environment</param>
    /// <returns>Path to JSON file</returns>
    private static string GetJsonFile(string pathFile, IHostEnvironment environment)
    {
        if (environment.ContentRootPath == "/app/")
            return Path.Combine(environment.ContentRootPath, pathFile);

        var directoryInfo = new DirectoryInfo(environment.ContentRootPath);
        var configPath = GetParent(directoryInfo)?.FullName;
        
        if (!string.IsNullOrEmpty(configPath))
            return Path.Combine(configPath, pathFile);
        
        Console.WriteLine($@"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
        return pathFile;

    }

    /// <summary>
    ///     Find parent root with name from value
    /// </summary>
    private static DirectoryInfo? GetParent(DirectoryInfo? directoryInfo)
    {
        while (true)
        {
            if (directoryInfo?.FullName.Contains("src") != true)
                return directoryInfo;

            directoryInfo = directoryInfo?.Parent;
        }
    }
}