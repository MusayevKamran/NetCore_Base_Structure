using App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Context.Interfaces;

/// <summary>
///     Contract for Database context
/// </summary>
public interface IAppDbContext
{
    DbSet<TodoModel> TodoModel { get; set; }
}