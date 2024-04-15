using TimeTracker.API.Services;
using TimeTracker.Database;

namespace TimeTracker.API.Tools;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDatabase (this IServiceCollection services)
    {
        services.AddDbContext<TimeTrackerContext>();

        services.AddScoped<TaskRepository>();
        services.AddScoped<TaskActionRepository>();

        return services;
    }

    internal static IServiceCollection AddServices (this IServiceCollection services)
    {
        services.AddTransient<HubMessanger>();

        return services;
    }
}