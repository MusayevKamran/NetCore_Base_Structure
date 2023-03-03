using App.Application.Models.Dto.Interfaces;
using App.Application.Models.Dto.Paginations;
using MediatR;

namespace App.Application.Models.Dto.Filter;

/// <summary>
///     Request to get logs by filter
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class FilterRequestDto<TFilter, TSort, TResponse> : IRequest<PageResponseDto<TResponse>>
    where TFilter : class, new() where TResponse : class
    where TSort : class, ISort, new()
{
    public FilterRequestDto()
    {
        Sort = new TSort();
        Filter = new TFilter();
        Pagination = new PaginationRequestDto();
    }
    
    /// <summary>
    ///     Pagination info
    /// </summary>
    public PaginationRequestDto Pagination { get; set; }
    
    /// <summary>
    ///     Filter info
    /// </summary>
    public TFilter Filter { get; set; }

    /// <summary>
    ///     Log filter. Sort model
    /// </summary>
    public TSort Sort { get; set; }
}