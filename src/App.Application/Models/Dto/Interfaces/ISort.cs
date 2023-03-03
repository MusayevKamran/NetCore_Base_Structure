using App.Common.Enums;

namespace App.Application.Models.Dto.Interfaces;

/// <summary>
///     Sorting Model
/// </summary>
public interface ISort
{
    /// <summary>
    ///     Sortable type
    /// </summary>
    SortableType SortableType { get; set; }
}