using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTracker.API.Database;
using Task = TimeTracker.API.Database.Models.TrackedTask;

namespace TimeTracker.API.Controllers;

[ApiController, Route("[controller]/")]
public sealed class TasksController : ControllerBase
{
    private readonly TimeTrackerContext _db;

    public TasksController(TimeTrackerContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Task>>> GetActiveTasks()
    {
        return await _db.Tasks.Where(task => !task.IsDone).ToListAsync();
    }

    [HttpGet("Done")]
    public async Task<ActionResult<List<Task>>> GetFinishedTasks ()
    {
        return await _db.Tasks.Where(task => task.IsDone).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Task>> PostTask ([FromBody] Task task)
    {
        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction("PostTask", task);
    }
}