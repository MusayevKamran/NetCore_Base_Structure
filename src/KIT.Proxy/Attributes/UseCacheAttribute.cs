namespace KIT.Proxy.Attributes;

/// <summary>
///     Attribute to use cache when calling method.
///     Only works for asynchronous methods.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class UseCacheAttribute : Attribute
{
    /// <summary>
    ///     Cache lifetime in seconds
    /// </summary>
    public int Lifetime { get; set; } = 50;

    /// <summary>
    ///     Use method parameters to cache data
    /// </summary>
    public bool UseParams { get; set; } = true;

    /// <summary>
    ///     The key to store data in the cache
    /// </summary>
    public string? Key { get; set; }
}