namespace App.Setup.AppSettings.Interfaces;

/// <summary>
///     Configuration section of swagger
/// </summary>
public interface ISwaggerSettings
{
    /// <summary>
    ///     XML comments for swagger
    /// </summary>
    public string[] XmlComments { get; set; }
}