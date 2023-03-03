using App.Application.Handlers;
using App.Domain.Models;
using KIT.Proxy.Attributes;

namespace App.Application.Proxy.impl;

public class ProxyService  : IProxyService
{
    [UseCache(Key = "ProxyService.ExecuteAsync", Lifetime = 600)]
    public Task<TodoModel> ExecuteAsync()
    {
        Thread.Sleep(5000);
        
        return Task.FromResult(new TodoModel() { Id = 1, Name = "Proxy Test" });
    }
}