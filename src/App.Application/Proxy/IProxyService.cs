using App.Domain.Models;

namespace App.Application.Proxy;

public interface IProxyService
{
    Task<TodoModel> ExecuteAsync();
}