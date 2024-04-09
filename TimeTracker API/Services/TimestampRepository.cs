using TimeTracker.Database;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Services;

public class TimestampRepository (TimeTrackerContext db) : IRepository<Timestamp>
{
    private readonly TimeTrackerContext _db = db;

    public async Task<Timestamp> AddAsync (Timestamp entity)
    {
        await _db.Timestamps.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
