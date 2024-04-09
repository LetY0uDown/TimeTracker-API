using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TimeTracker.API.Services;
using TimeTracker.Database;
using TimeTracker.Database.Models;
using Task = TimeTracker.Database.Models.TrackedTask;

namespace TimeTracker.API.Controllers;

[ApiController, Route("[controller]/")]
public sealed class TasksController (TimeTrackerContext db, IHubContext<MainHub> hub,
                                     TaskRepository taskRepository,
                                     TimestampRepository timestampRepository) : ControllerBase
{
    private readonly TimeTrackerContext _db = db;
    private readonly IHubContext<MainHub> _hub = hub;
    private readonly TaskRepository _taskRepository = taskRepository;
    private readonly TimestampRepository _timestampRepository = timestampRepository;

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
    public async Task<ActionResult> PostTask ([FromBody] Task task)
    {
        await _taskRepository.AddAsync(task);

        await _hub.Clients.All.SendAsync("TaskPosted", task);

        return Ok();
    }

    [HttpPut("{id:int}/start")]
    public async Task<ActionResult> StartTask ([FromRoute] int id)
    {
        var timestamp = await _timestampRepository.AddAsync(new Timestamp {
            TaskId = id,
            TypeId = 1,
            CreatedAt = DateTime.Now.Ticks
        });

        await _hub.Clients.All.SendAsync("TaskStateUpdated", timestamp);

        return Ok();
    }

    [HttpPut("{id:int}/resume")]
    public async Task<ActionResult> ResumeTask ([FromRoute] int id)
    {
        var timestamp = await _timestampRepository.AddAsync(new Timestamp {
            TaskId = id,
            TypeId = 3,
            CreatedAt = DateTime.Now.Ticks
        });

        await _hub.Clients.All.SendAsync("TaskStateUpdated", timestamp);

        return Ok();
    }

    [HttpPut("{id:int}/pause")]
    public async Task<ActionResult> PauseTask ([FromRoute] int id)
    {
        var timestamp = await _timestampRepository.AddAsync(new Timestamp {
            TaskId = id,
            TypeId = 2,
            CreatedAt = DateTime.Now.Ticks
        });

        await _hub.Clients.All.SendAsync("TaskStateUpdated", timestamp);

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
        _db.Entry(task).State = EntityState.Modified;

        var timestamp = await _timestampRepository.AddAsync(new Timestamp {
            TaskId = id,
            TypeId = 4,
            CreatedAt = DateTime.Now.Ticks
        });

        await _hub.Clients.All.SendAsync("TaskStateUpdated", timestamp);

        return Ok();
    }
    
    [HttpGet("{id:int}/timestamps")]
    public async Task<ActionResult<List<Timestamp>>> GetTimestampsForTask ([FromRoute] int id)
    {
        return await _db.Timestamps.Include(ts => ts.Type).Where(ts => ts.TaskId == id).ToListAsync();
    }
}