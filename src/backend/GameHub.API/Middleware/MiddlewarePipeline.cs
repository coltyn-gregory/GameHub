using Scalar.AspNetCore;

namespace GameHub.API.Middleware;

internal static class MiddlewarePipeline
{
    public static void UseMiddlewarePipeline(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        app.UseAuthorization();
        app.MapControllers();
    }
}
