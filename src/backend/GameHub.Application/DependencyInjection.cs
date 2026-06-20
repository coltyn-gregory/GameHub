using Microsoft.Extensions.DependencyInjection;

namespace GameHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}