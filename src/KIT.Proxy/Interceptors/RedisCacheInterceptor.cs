using App.Common.Enums;
using App.Common.Extensions;
using Castle.DynamicProxy;
using KIT.Proxy.Attributes;
using KIT.Redis.Interfaces;
using KIT.Redis.Models;
using Newtonsoft.Json;

namespace KIT.Proxy.Interceptors;

/// <summary>
///     Redis cache interceptor
/// </summary>
internal class RedisCacheInterceptor : AsyncInterceptorBase, IRedisCacheInterceptor
{
    private readonly IRedisRepository _redisRepository;

    public RedisCacheInterceptor(IRedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }


    /// <summary>
    ///     Override in derived classes to intercept method invocations.
    /// </summary>
    /// <param name="invocation">The method invocation.</param>
    /// <param name="proceedInfo">The <see cref="IInvocationProceedInfo"/>.</param>
    /// <param name="proceed">The function to proceed the <paramref name="proceedInfo"/>.</param>
    /// <returns>A <see cref="Task" /> object that represents the asynchronous operation.</returns>
    protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        => await proceed(invocation, proceedInfo).ConfigureAwait(false);

    /// <summary>
    ///     Override in derived classes to intercept method invocations.
    /// </summary>
    /// <typeparam name="TResult">The type of the <see cref="Task{T}"/> <see cref="Task{T}.Result"/>.</typeparam>
    /// <param name="invocation">The method invocation.</param>
    /// <param name="proceedInfo">The <see cref="IInvocationProceedInfo"/>.</param>
    /// <param name="proceed">The function to proceed the <paramref name="proceedInfo"/>.</param>
    /// <returns>A <see cref="Task" /> object that represents the asynchronous operation.</returns>
    protected override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        => Intercept(invocation, proceedInfo, proceed);

    /// <summary>
    ///     Intercept method
    /// </summary>
    /// <typeparam name="TResult">The type of the <see cref="Task{T}"/> <see cref="Task{T}.Result"/>.</typeparam>
    /// <param name="invocation">The method invocation.</param>
    /// <param name="proceedInfo">The <see cref="IInvocationProceedInfo"/>.</param>
    /// <param name="proceed">The function to proceed the <paramref name="proceedInfo"/>.</param>
    /// <returns>A <see cref="Task" /> object that represents the asynchronous operation.</returns>
    private async Task<TResult> Intercept<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        if (GetCacheAttribute(invocation) is not UseCacheAttribute cacheAttribute)
            return await proceed(invocation, proceedInfo);

        var cacheKey = GenerateCacheKey(invocation, cacheAttribute).GetHash(HashType.MD5);
        var cacheValue = await _redisRepository.GetAsync<RedisCacheDataModel<TResult>>(cacheKey);

        if (cacheValue is not null)
            return cacheValue.Data;

        var returnValue = await proceed(invocation, proceedInfo).ConfigureAwait(false);

        await _redisRepository.SetAsync(cacheKey, new RedisCacheDataModel<TResult>(returnValue), TimeSpan.FromSeconds(cacheAttribute.Lifetime));

        return returnValue;

    }

    /// <summary>
    ///     Generate cache key
    /// </summary>
    /// <param name="invocation">The method invocation</param>
    /// <param name="useCacheAttribute">Cache attribute</param>
    /// <returns>Cache key</returns>
    private static string GenerateCacheKey(IInvocation invocation, UseCacheAttribute useCacheAttribute)
    {
        var masterCacheKey = !string.IsNullOrEmpty(useCacheAttribute.Key)
            ? useCacheAttribute.Key
            : invocation.Method.Name;

        if (invocation.Arguments == null || invocation.Arguments.Length == 0 || !useCacheAttribute.UseParams)
            return masterCacheKey;

        return $"{masterCacheKey}--{string.Join("--", SerializeMethodArguments(invocation.Arguments))}";
    }

    /// <summary>
    ///     Serialize method arguments to json
    /// </summary>
    /// <param name="arguments">Method arguments</param>
    /// <returns>Serialized method arguments.</returns>
    private static IEnumerable<string> SerializeMethodArguments(IEnumerable<object?> arguments) =>
        arguments.Select(argument =>
        {
            if (argument is null)
                return "**NULL**";

            return argument is CancellationToken ? string.Empty : JsonConvert.SerializeObject(argument);
        });

    /// <summary>
    ///     Get the cache attribute
    /// </summary>
    /// <param name="invocation">The method invocation</param>
    /// <returns>Cache attribute</returns>
    private object? GetCacheAttribute(IInvocation invocation) => invocation.MethodInvocationTarget
        .GetCustomAttributes(typeof(UseCacheAttribute), false)
        .FirstOrDefault();
}