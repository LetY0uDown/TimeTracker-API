using TimeTracker.API.Services;
using TimeTracker.Database;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Tools;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDatabase (this IServiceCollection services)
    {
        services.AddDbContext<TimeTrackerContext>();

        services.AddScoped<IRepository<TrackedTask>, TaskRepository>();
        services.AddScoped<IRepository<TaskAction>, TaskActionRepository>();

        return services;
    }

    internal static IServiceCollection AddServices (this IServiceCollection services)
    {
        services.AddTransient<HubMessanger>();

        return services;
    }
}