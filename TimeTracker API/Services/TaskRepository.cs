using Microsoft.EntityFrameworkCore;
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

    public async Task<TrackedTask?> GetByIDAsync (int id)
    {
        return await _db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
    }

    public async Task UpdateAsync (TrackedTask entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public List<TrackedTask> Where (Func<TrackedTask, bool> predicate)
    {
        return _db.Tasks.Where(predicate).ToList();
    }
}
