using KIT.MediatR.Arguments;
using KIT.MediatR.PipelineBehaviors.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KIT.MediatR.PipelineBehaviors;

/// <summary>
///     Pipeline behavior registrar
/// </summary>
public static class PipelineBehaviorRegistrar
{
    /// <summary>
    ///     Register pipeline behaviors
    /// </summary>
    /// <param name="services">Services сollection</param>
    /// <param name="pipelineBehaviorType">Type of Pipeline behavior</param>
    /// <param name="handlerFilteringFunc">Functions for filtering handlers for which pipeline needs to be registered</param>
    public static void RegisterPipelineBehaviors(this IServiceCollection services, Type pipelineBehaviorType, Func<UsePipelineBehaviorsAttribute, bool> handlerFilteringFunc)
        => HandlerArguments.GetArgumentsThatUsePipelines()
            .Where(handlerArguments => handlerFilteringFunc(handlerArguments.UsePipelineAttribute)).ToList()
            .ForEach(handlerArguments =>
            {
                var pipelineBehaviorBaseType = typeof(IPipelineBehavior<,>).MakeGenericType(handlerArguments.RequestType, handlerArguments.ResponseType);
                var currentPipelineBehaviorType = pipelineBehaviorType.MakeGenericType(handlerArguments.RequestType, handlerArguments.ResponseType);
                services.AddScoped(pipelineBehaviorBaseType, currentPipelineBehaviorType);
            });
}