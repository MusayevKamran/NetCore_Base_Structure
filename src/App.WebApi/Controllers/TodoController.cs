using App.Application.Models;
using App.Application.Models.Dto;
using App.Application.Models.Dto.Filter;
using App.Application.Models.Dto.Sort;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using mediaType = System.Net.Mime.MediaTypeNames.Application;

namespace App.WebApi.Controllers;

[ApiController]
[Route("todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("")]
    [Produces(mediaType.Json, Type = typeof(PageResponseDto<TodoModelResponseDto>))]
    public async Task<PageResponseDto<TodoModelResponseDto>> Test(FilterRequestDto<TodoModelFilterDto, TodoModelSort, TodoModelResponseDto> request, CancellationToken cancellationToken)
    => await _mediator.Send(request, cancellationToken);
}