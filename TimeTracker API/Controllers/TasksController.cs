using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Services;
using TimeTracker.Database.Models;
using TimeTracker.Database.Services;

namespace TimeTracker.API.Controllers;

[ApiController, Route("[controller]/")]
public sealed class TasksController (HubMessanger hub,
                                     IRepository<TrackedTask> taskRepository,
                                     IRepository<TaskAction> actionsRepository) : ControllerBase
{
    private readonly HubMessanger _hub = hub;

    private readonly IRepository<TrackedTask> _taskRepository    = taskRepository;
    private readonly IRepository<TaskAction>  _actionsRepository = actionsRepository;

    [HttpGet]
    public ActionResult<List<TrackedTask>> GetActiveTasks ()
    {
        return _taskRepository.Where(task => !task.IsDone).ToList();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrackedTask?>> GetTaskByID (int id)
    {
        return await _taskRepository.GetByIDAsync(id);
    }

    [HttpGet("Done")]
    public ActionResult<List<TrackedTask>> GetFinishedTasks ()
    {
        return _taskRepository.Where(task => task.IsDone).ToList();
    }

    [HttpPost]
    public async Task<ActionResult> PostTask ([FromBody] TrackedTask task)
    {
        await _taskRepository.AddAsync(task);
        await _hub.TaskCreatedAsync(task);

        return Ok();
    }

    [HttpPut("{id:int}/start")]
    public async Task<ActionResult> StartTask ([FromRoute] int id)
    {
        var action = await _actionsRepository.AddAsync(new TaskAction {
            Id = 0,
            TaskId = id,
            Task = null!,
            Type = null,
            TypeId = TaskActionType.Kind.Start,
            CreatedAt = DateTime.Now.Ticks
        });

        action = await _actionsRepository.GetByIDAsync(action.Id);
        await _hub.TaskUpdatedAsync(action!);

        return Ok();
    }

    [HttpPut("{id:int}/resume")]
    public async Task<ActionResult> ResumeTask ([FromRoute] int id)
    {
        var action = await _actionsRepository.AddAsync(new TaskAction {
            TaskId = id,
            TypeId = TaskActionType.Kind.Resume,
            CreatedAt = DateTime.Now.Ticks
        });

        action = await _actionsRepository.GetByIDAsync(action.Id);
        await _hub.TaskUpdatedAsync(action!);

        return Ok();
    }

    [HttpPut("{id:int}/pause")]
    public async Task<ActionResult> PauseTask ([FromRoute] int id)
    {
        var action = await _actionsRepository.AddAsync(new TaskAction {
            Id = 0,
            TaskId = id,
            Task = null!,
            Type = null,
            TypeId = TaskActionType.Kind.Pause,
            CreatedAt = DateTime.Now.Ticks
        });

        action = await _actionsRepository.GetByIDAsync(action.Id);
        await _hub.TaskUpdatedAsync(action!);

        return Ok();
    }

    [HttpPut("{id:int}/finish")]
    public async Task<ActionResult> FinishTask ([FromRoute] int id)
    {
        var task = await _taskRepository.GetByIDAsync(id);
        if (task is null) {
            return NotFound("Задание не найдено");
        }

        task.IsDone = true;
        await _taskRepository.UpdateAsync(task);

        var action = await _actionsRepository.AddAsync(new TaskAction {
            TaskId = id,
            TypeId = TaskActionType.Kind.Finish,
            CreatedAt = DateTime.Now.Ticks
        });

        action = await _actionsRepository.GetByIDAsync(action.Id);
        await _hub.TaskUpdatedAsync(action!);

        return Ok();
    }

    [HttpGet("{id:int}/actions")]
    public ActionResult<List<TaskAction>> GetactionsForTask ([FromRoute] int id)
    {
        return _actionsRepository.Where(action => action.TaskId == id).ToList();
    }
}