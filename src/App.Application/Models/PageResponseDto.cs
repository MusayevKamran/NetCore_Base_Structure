using System.Text.Json.Serialization;
using App.Application.Models.Dto.Paginations;

namespace App.Application.Models;

/// <summary>
///     Response format
/// </summary>
public class PageResponseDto<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResponseDto{T}" /> class.
    /// </summary>
    public PageResponseDto(PaginationRequestDto paginationRequest, long totalCount, IEnumerable<T> result)
    {
        Pagination = new PaginationResponseDto(totalCount, paginationRequest.PageNumber, paginationRequest.PageSize, result.Count());
        List = result;
    }

    [JsonConstructor]
    public PageResponseDto(PaginationResponseDto pagination, IEnumerable<T> list)
    {
        Pagination = pagination;
        List = list;
    }
    

    /// <summary>
    ///     Navigation Information
    /// </summary>
    public PaginationResponseDto Pagination { get; }

    /// <summary>
    ///     Array of received objects
    /// </summary>
    public IEnumerable<T> List { get; }
}