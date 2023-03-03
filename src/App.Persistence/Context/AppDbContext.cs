using App.Domain.Models;
using App.Persistence.Context.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Context;

public class AppDbContext: DbContext, IAppDbContext
{
    public DbSet<TodoModel> TodoModel { get; set; }
    
    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="options">Database options</param>
    public AppDbContext(DbContextOptions options) : base(options) {  }

    /// <summary>
    ///     Apply configurations 
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

}