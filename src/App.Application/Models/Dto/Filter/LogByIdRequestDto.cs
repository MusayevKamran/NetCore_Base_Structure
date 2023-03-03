using System.ComponentModel.DataAnnotations;
using MediatR;

namespace App.Application.Models.Dto.Filter;

/// <summary>
///     Request model of a request to get a log entry by id
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public class LogByIdRequestDto<TResponse> : IRequest<TResponse?>
{
    public LogByIdRequestDto(string id)
    {
        Id = id;
    }

    /// <summary>
    ///     Row Id
    /// </summary>
    [Required]
    public string Id { get; set; }
}