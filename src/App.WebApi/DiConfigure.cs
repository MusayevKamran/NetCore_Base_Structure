using KIT.MediatR;
using KIT.Proxy;
using KIT.Redis;

namespace App.WebApi;

/// <summary>
///     DI configuration for api
/// </summary>
public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, string environmentName)
    {

        services.ConfigureRedis();
        services.ConfigureProxy();
        
        // services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        // services.AddSingleton<ITokenService, TokenService>();
        
        Application.DiConfigure.AddApplication(services);
        Persistence.DiConfigure.AddPersistence(services);
    }
}