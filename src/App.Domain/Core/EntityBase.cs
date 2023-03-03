namespace App.Domain.Core;

/// <summary>
/// Base class for all entities
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Unique entity identifier
    /// </summary>
    public virtual int Id { get; set; }
}