using System.Text.Json.Serialization;
using TimeTracker.API.Services;
using TimeTracker.Database;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Tools;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет контроллеры и SignalR. Настраивает для них JSON
    /// </summary>
    internal static IServiceCollection ConfigureAPI (this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
        services.AddSignalR().AddJsonProtocol(opt => {
            opt.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        return services;
    }

    /// <summary>
    /// Добавляет классы для работы с БД
    /// </summary>
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