using App.Application.Models.Dto.Interfaces;
using App.Common.Enums;

namespace App.Application.Models.Dto.Sort;

public class TodoModelSort: ISort
{
    public SortableType SortableType { get; set; }
}