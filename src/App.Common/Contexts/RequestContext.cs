namespace App.Common.Contexts;

/// <summary>
///     Request context
/// </summary>
public class RequestContext
{
    /// <summary>
    ///     Language for localization
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    ///     Auth token
    /// </summary>
    public string? Token { get; set; }
}