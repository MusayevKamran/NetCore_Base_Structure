using App.Application.Middleware;
using App.Persistence.Context;
using App.Setup;
using App.Setup.ServiceConfiguration;
using App.WebApi;
using AuditService.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigs();
builder.ConfigureSerilog();

try
{
    builder.Services.RegisterSettings();
    builder.Services.AddControllersWithFilters();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AdditionalConfigurations();
    builder.Services.AddSwagger();
    builder.Services.RegisterServices(builder.Environment.EnvironmentName);

    var app = builder.Build();

    // Todo migrate database
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    if (!app.Environment.IsProduction())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
    }
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseRouting();
    app.UseAuthorization();

    app.UseMiddleware<AppMiddlewareException>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Stopped program because of exception: {FullMessage}", ex.FullMessage());
}
finally
{
    Log.CloseAndFlush();
}