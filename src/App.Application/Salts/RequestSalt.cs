using App.Common.Contexts;
using KIT.MediatR.Salts;

namespace App.Application.Salts;

/// <summary>
///     Request salt data
///     Used in request pipelines to generate a cache key.
/// </summary>

internal class RequestSalt : IRequestSalt
{
    private readonly RequestContext _requestContext;

    public RequestSalt(RequestContext requestContext)
    {
        _requestContext = requestContext;
    }

    /// <summary>
    ///     Get request salt data
    /// </summary>
    /// <returns>Request salt data</returns>

    public string GetSalt() => $"{_requestContext.Token};{_requestContext.Language}";
}