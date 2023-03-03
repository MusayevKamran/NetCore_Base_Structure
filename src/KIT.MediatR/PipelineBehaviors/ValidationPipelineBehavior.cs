using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KIT.MediatR.PipelineBehaviors;

/// <summary>
///     Handler pipeline element for request validation
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationPipelineBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Perform request prehandling for validation
    /// </summary>
    /// <param name="request">Request for validation</param>
    /// <param name="next">The request handling pipeline delegate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response after request handling</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_serviceProvider.GetService<IValidator<TRequest>>() is var validator && validator is null)
            return await next();

        if (await validator.ValidateAsync(request, cancellationToken) is var validationResult && validationResult.IsValid)
            return await next();

        throw new ArgumentException(string.Join(' ', validationResult.Errors.Select(error => error.ErrorMessage)));
    }
}