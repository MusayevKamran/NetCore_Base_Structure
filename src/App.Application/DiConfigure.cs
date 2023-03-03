using System.Reflection;
using App.Application.Handlers;
using App.Application.Proxy;
using App.Application.Proxy.impl;
using KIT.MediatR.PipelineBehaviors;
using KIT.Proxy;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace App.Application;

/// <summary>
///     Dependency injection
/// </summary>
public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void AddApplication(IServiceCollection services)
    {

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.RegisterPipelineBehaviors(typeof(LogPipelineBehavior<,>), attribute => attribute.UseLogging);
        services.RegisterPipelineBehaviors(typeof(ValidationPipelineBehavior<,>), attribute => attribute.UseValidation);
        // services.RegisterPipelineBehaviors(typeof(CachePipelineBehavior<,>), attribute => attribute.UseCache);
        // services.AddScoped<IRequestSalt, RequestSalt>();
        services.AddProxiedCacheScoped<IProxyService, ProxyService>();
    }
}