namespace KIT.MediatR.Salts;

/// <summary>
///     Request salt data
///     Used in request pipelines to generate a cache key.
/// </summary>
public interface IRequestSalt
{
    /// <summary>
    ///     Get request salt data
    /// </summary>
    /// <returns>Request salt data</returns>
    string GetSalt();
}