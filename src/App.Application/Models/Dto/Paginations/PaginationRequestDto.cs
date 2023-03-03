using System.ComponentModel.DataAnnotations;

namespace App.Application.Models.Dto.Paginations;

/// <summary>
///     Navigation information
/// </summary>
public class PaginationRequestDto
{
    public PaginationRequestDto()
    {
        PageSize = 20;
        PageNumber = 0;
    }

    /// <summary>
    ///     The number of elements per page
    /// </summary>
    [Required]
    public int PageSize { get; set; }

    /// <summary>
    ///     Current page number. 
    ///     The page number starts at 0.
    /// </summary>
    [Required]
    public int PageNumber { get; set; }

    /// <summary>
    ///     Define offset
    /// </summary>
    /// <returns>Offset from the first result to fetch</returns>
    public int GetOffset() => PageNumber * PageSize;
}