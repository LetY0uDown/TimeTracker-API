using Microsoft.EntityFrameworkCore;
using TimeTracker.Database;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Services;

public class TaskActionRepository (TimeTrackerContext db) : IRepository<TaskAction>
{
    private readonly TimeTrackerContext _db = db;

    public async Task<TaskAction> AddAsync (TaskAction entity)
    {
        await _db.TaskActions.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public Task<TaskAction?> GetByIDAsync (int id)
    {
        return _db.TaskActions.Include(action => action.Type).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync (TaskAction entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public List<TaskAction> Where (Func<TaskAction, bool> predicate)
    {
        return _db.TaskActions.Include(action => action.Type).Where(predicate).ToList();
    }
}
