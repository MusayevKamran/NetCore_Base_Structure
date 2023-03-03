using Castle.DynamicProxy;
using KIT.Proxy.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace KIT.Proxy;

/// <summary>
///     Proxy configurator
/// </summary>
public static class ProxyConfigurator
{
    /// <summary>
    ///     Configure proxy
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureProxy(this IServiceCollection services)
    {
        services.AddSingleton(new ProxyGenerator());
        services.AddScoped<IRedisCacheInterceptor, RedisCacheInterceptor>();
    }

    /// <summary>
    ///     Adding a proxy for a service.
    ///     The proxy is the Redis cache service.
    ///     Only works for asynchronous methods.
    /// </summary>
    /// <typeparam name="TInterface">Type of interface</typeparam>
    /// <typeparam name="TImplementation">Type of implementation</typeparam>
    /// <param name="services">Services сollection</param>
    public static void AddProxiedCacheScoped<TInterface, TImplementation>
        (this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddScoped<TImplementation>();
        services.AddProxiedCacheService<TInterface, TImplementation>();
    }

    /// <summary>
    ///     Adding a proxy for a service.
    ///     The proxy is the Redis cache service.
    ///     Only works for asynchronous methods.
    /// </summary>
    /// <typeparam name="TInterface">Type of interface</typeparam>
    /// <typeparam name="TImplementation">Type of implementation</typeparam>
    /// <param name="services">Services сollection</param>
    /// <param name="implimintationFactory">The factory that creates the service.</param>
    public static void AddProxiedCacheScoped<TInterface, TImplementation>
        (this IServiceCollection services, Func<IServiceProvider, TImplementation> implimintationFactory)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddScoped(implimintationFactory);
        services.AddProxiedCacheService<TInterface, TImplementation>();
    }

    /// <summary>
    ///     Adding a proxy for a service.
    ///     The proxy is the Redis cache service.
    ///     Only works for asynchronous methods.
    /// </summary>
    /// <typeparam name="TImplementation">Type of implementation</typeparam>
    /// <param name="services">Services сollection</param>
    /// <param name="implimintationFactory">The factory that creates the service.</param>
    public static void AddProxiedCacheScoped<TImplementation>
        (this IServiceCollection services, Func<IServiceProvider, TImplementation> implimintationFactory)
        where TImplementation : class
        => services.AddScoped(typeof(TImplementation), serviceProvider =>
        {
            var proxyGenerator = serviceProvider
                .GetRequiredService<ProxyGenerator>();

            var actual = implimintationFactory(serviceProvider);

            var interceptor = serviceProvider
                .GetRequiredService<IRedisCacheInterceptor>();

            return proxyGenerator.CreateClassProxyWithTarget(typeof(TImplementation), actual, interceptor);
        });


    /// <summary>
    ///     Add proxied cache service
    /// </summary>
    /// <typeparam name="TInterface">Type of interface</typeparam>
    /// <typeparam name="TImplementation">Type of implementation</typeparam>
    /// <param name="services">Services сollection</param>
    private static void AddProxiedCacheService<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        => services.AddScoped(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider
                .GetRequiredService<ProxyGenerator>();

            var actual = serviceProvider
                .GetRequiredService<TImplementation>();

            var interceptor = serviceProvider
                .GetRequiredService<IRedisCacheInterceptor>();

            return proxyGenerator.CreateInterfaceProxyWithTarget(
                typeof(TInterface), actual, interceptor);
        });
}