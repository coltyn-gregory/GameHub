using GameHub.API.Middleware;
using GameHub.Application;
using GameHub.Persistence;

namespace GameHub.API;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder =
            WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplicationServices()
            .AddApiServices()
            .AddPersistenceServices(builder.Configuration);

        WebApplication app =
            builder.Build();

        if (app.Environment.IsDevelopment())
        {
            await app.Services.ApplyMigrationsAsync();
            await app.Services.SeedAsync();
        }

        app.UseMiddlewarePipeline();

        await app.RunAsync();
    }
}
