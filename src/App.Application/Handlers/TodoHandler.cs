using App.Application.Models;
using App.Application.Models.Dto;
using App.Application.Models.Dto.Filter;
using App.Application.Models.Dto.Paginations;
using App.Application.Models.Dto.Sort;
using App.Application.Proxy;
using App.Domain.Models;
using App.Persistence.Context.Interfaces;
using KIT.MediatR.PipelineBehaviors.Attributes;
using KIT.Redis.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace App.Application.Handlers;

[UsePipelineBehaviors(UseCache = true, UseLogging = true, CacheLifeTime = 120, UseValidation = true)]
public class TodoHandler : IRequestHandler<FilterRequestDto<TodoModelFilterDto, TodoModelSort, TodoModelResponseDto>, PageResponseDto<TodoModelResponseDto>>
{
    private readonly ILogger _logger;
    private readonly IAppDbContext _appDbContext;
    private readonly IRedisRepository _redisRepository;
    private readonly IProxyService _proxyService;
    
    public TodoHandler(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger>();
        _appDbContext = serviceProvider.GetRequiredService<IAppDbContextFactory>().CreateContext();
        _redisRepository = serviceProvider.GetRequiredService<IRedisRepository>();
        _proxyService = serviceProvider.GetRequiredService<IProxyService>();
    }
    

    public async Task<PageResponseDto<TodoModelResponseDto>> Handle(FilterRequestDto<TodoModelFilterDto, TodoModelSort, TodoModelResponseDto> request, CancellationToken cancellationToken)
    {
        var proxyData = await _proxyService.ExecuteAsync();
        
        var key = "todoModel1";
        
        TodoModel data = await _redisRepository.GetAsync<TodoModel>(key);
        
        if (data == null)
        {
            await _redisRepository.SetAsync(key, proxyData);
            
            data = await _redisRepository.GetAsync<TodoModel>(key);
        }


        var result = _appDbContext.TodoModel.FirstOrDefault();

        var list = new List<TodoModelResponseDto>()
        {
            new()
            {
                Id = result.Id,
                Name = result.Name + " " + data.Name
            }
        };
        
        var pagination = new PaginationRequestDto()
        {
            PageNumber = 3,
            PageSize = 4,
        };

        var model = new PageResponseDto<TodoModelResponseDto>(pagination, 1, list);
        
        _logger.Information("This is test", model);

        return new PageResponseDto<TodoModelResponseDto>(model.Pagination, model.List);
    }
}