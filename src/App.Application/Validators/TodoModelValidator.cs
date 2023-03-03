using App.Application.Models.Dto;
using App.Application.Models.Dto.Filter;
using App.Application.Models.Dto.Paginations;
using App.Application.Models.Dto.Sort;
using FluentValidation;

namespace App.Application.Validators;

public class TodoModelValidator: RequestBaseValidator<TodoModelFilterDto, TodoModelSort, TodoModelResponseDto>
{
    public TodoModelValidator(IValidator<PaginationRequestDto> paginationRequestValidator) : base(paginationRequestValidator)
    {
    }
}