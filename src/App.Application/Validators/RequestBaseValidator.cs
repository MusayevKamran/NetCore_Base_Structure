using App.Application.Models.Dto.Filter;
using App.Application.Models.Dto.Interfaces;
using App.Application.Models.Dto.Paginations;
using FluentValidation;

namespace App.Application.Validators;

/// <summary>
///     Base log request validator
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public abstract class RequestBaseValidator<TFilter, TSort, TResponse> 
    : AbstractValidator<FilterRequestDto<TFilter, TSort, TResponse>>
    where TFilter : class, ILogFilter, new()
    where TSort : class, ISort, new()
    where TResponse : class
{
    protected RequestBaseValidator(IValidator<PaginationRequestDto> paginationRequestValidator)
    {
        RuleFor(model => model.Pagination).SetValidator(paginationRequestValidator);
    }
}