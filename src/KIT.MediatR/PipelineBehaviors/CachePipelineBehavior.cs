using App.Common.Enums;
using App.Common.Extensions;
using KIT.MediatR.Arguments;
using KIT.MediatR.PipelineBehaviors.Attributes;
using KIT.MediatR.Salts;
using KIT.Redis.Interfaces;
using KIT.Redis.Models;
using MediatR;
using Newtonsoft.Json;


namespace KIT.MediatR.PipelineBehaviors;

/// <summary>
///     Handler pipeline element for request caching
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IRedisRepository _redisRepository;

    private readonly IRequestSalt _requestSalt;

    public CachePipelineBehavior(IRedisRepository redisRepository, IRequestSalt requestSalt )
    {
        _redisRepository = redisRepository;
        _requestSalt = requestSalt;
    }

    /// <summary>
    ///     Handle the request and apply caching if necessary
    /// </summary>
    /// <param name="request">Request for caching</param>
    /// <param name="next">The request handling pipeline delegate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response after request handling</returns>
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (GetPipelineBehaviorsAttribute() is not UsePipelineBehaviorsAttribute usePipelineBehaviorsAttribute)
            return await next();

        var cacheKey = GenerateCacheKey(request);
        var cacheValue = await _redisRepository.GetAsync<RedisCacheDataModel<TResponse>>(cacheKey);

        if (cacheValue is not null)
            return cacheValue.Data;

        var returnValue = await next();
        await _redisRepository.SetAsync(cacheKey, new RedisCacheDataModel<TResponse>(returnValue), TimeSpan.FromSeconds(usePipelineBehaviorsAttribute.CacheLifeTime));
        return returnValue;
    }

    /// <summary>
    ///     Generate a key to store the cache
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns>Key to store the cache</returns>
    private string? GenerateCacheKey(TRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var key = $"{_requestSalt.GetSalt()};{request.GetType().Name}--{json}";
        return key.GetHash(HashType.MD5);
    }

    /// <summary>
    ///     Get the "UsePipelineBehaviors" attribute
    /// </summary>
    /// <returns>"UsePipelineBehaviors" attribute</returns>
    private static object GetPipelineBehaviorsAttribute() => 
        HandlerArguments.GetArgumentsThatUsePipelines().FirstOrDefault(arguments =>
        arguments.RequestType == typeof(TRequest) && arguments.ResponseType == typeof(TResponse)).UsePipelineAttribute;
}