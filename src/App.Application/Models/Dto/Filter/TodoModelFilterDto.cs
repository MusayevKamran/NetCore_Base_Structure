using App.Application.Models.Dto.Interfaces;

namespace App.Application.Models.Dto.Filter;

public class TodoModelFilterDto: ILogFilter
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}