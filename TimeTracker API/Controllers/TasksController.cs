using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Database;
using TimeTracker.Database.Models;
using Task = TimeTracker.Database.Models.TrackedTask;

namespace TimeTracker.API.Controllers;

[ApiController, Route("[controller]/")]
public sealed class TasksController (TimeTrackerContext db) : ControllerBase
{
    private readonly TimeTrackerContext _db = db;

    [HttpGet]
    public async Task<ActionResult<List<Task>>> GetActiveTasks ()
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
        task.IsPaused = true;
        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction("PostTask", task);
    }

    [HttpPut("{id:int}/start")]
    public async Task<ActionResult> StartTask ([FromRoute] int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) {
            return NotFound("Задание не найдено");
        }

        task.IsPaused = false;
        _db.Entry(task).State = EntityState.Modified;

        var Timestamp = new Timestamp {
            TaskId = task.Id,
            TypeId = 1,
            CreatedAt = DateTime.Now.Ticks
        };
        await _db.Timestamps.AddAsync(Timestamp);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}/resume")]
    public async Task<ActionResult> ResumeTask ([FromRoute] int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) {
            return NotFound("Задание не найдено");
        }

        task.IsPaused = false;
        _db.Entry(task).State = EntityState.Modified;

        var Timestamp = new Timestamp {
            TaskId = task.Id,
            TypeId = 3,
            CreatedAt = DateTime.Now.Ticks
        };

        await _db.Timestamps.AddAsync(Timestamp);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}/pause")]
    public async Task<ActionResult> PauseTask ([FromRoute] int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) {
            return NotFound("Задание не найдено");
        }

        task.IsPaused = true;
        _db.Entry(task).State = EntityState.Modified;

        var now = DateTime.Now;

        var Timestamp = new Timestamp {
            TaskId = task.Id,
            TypeId = 2,
            CreatedAt = now.Ticks
        };

        await _db.Timestamps.AddAsync(Timestamp);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}/finish")]
    public async Task<ActionResult> FinishTask ([FromRoute] int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) {
            return NotFound("Задание не найдено");
        }

        task.IsDone = true;
        task.IsPaused = true;
        _db.Entry(task).State = EntityState.Modified;

        var Timestamp = new Timestamp {
            TaskId = task.Id,
            TypeId = 4,
            CreatedAt = DateTime.Now.Ticks
        };

        await _db.Timestamps.AddAsync(Timestamp);
        await _db.SaveChangesAsync();

        return Ok();
    }
    
    [HttpGet("{id:int}/timestamps")]
    public async Task<ActionResult<List<Timestamp>>> GetTimestampsForTask ([FromRoute] int id)
    {
        return await _db.Timestamps.Include(ts => ts.Type).Where(ts => ts.TaskId == id).ToListAsync();
    }
}