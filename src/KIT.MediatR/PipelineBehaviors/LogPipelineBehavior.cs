using MediatR;
using Serilog;

namespace KIT.MediatR.PipelineBehaviors;

/// <summary>
///     Handler pipeline element for request logging
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class LogPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LogPipelineBehavior(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Perform request prehandling for logging
    /// </summary>
    /// <param name="request">Request for logging</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">The request handling pipeline delegate</param>
    /// <returns>Response after request handling</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();
            _logger.Verbose("Handling request completed successfully", GenerateContextModel(request));
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Handling request ended with an error", GenerateContextModel(request));
            throw;
        }
    }

    /// <summary>
    ///     Generate a context model for logging
    /// </summary>
    /// <param name="request">Request for handling</param>
    /// <returns>Context model for logging</returns>
    private static object GenerateContextModel(TRequest request)
        => new
        {
            RequestName = typeof(TRequest).Name,
            Request = request
        };
}