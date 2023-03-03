using App.Application.Models.Dto.Interfaces;
using App.Common.Enums;

namespace App.Application.Models.Dto.Sort;

/// <summary>
///     Sort model
/// </summary>
public class SortRequestDto : ISort
{
    /// <summary>
    ///     Date and time of the event
    /// </summary>
    public SortableType SortableType { get; set; }
}