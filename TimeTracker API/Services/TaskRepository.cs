using TimeTracker.Database;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Services;

public class TaskRepository (TimeTrackerContext db) : IRepository<TrackedTask>
{
    private readonly TimeTrackerContext _db = db;

    public async Task<TrackedTask> AddAsync (TrackedTask entity)
    {
        await _db.Tasks.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
