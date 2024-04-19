namespace TimeTracker.Database.Models;

/// <summary>
/// Хранит записи о разных действиях с задачей. ID задача, когда сделано, тип действия
/// </summary>
public class TaskAction
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    /// <summary>
    /// Время создания действия. Указывается в тиках
    /// </summary>
    public long CreatedAt { get; set; }

    public TaskActionType.Kind? TypeId { get; set; }

    /// <summary>
    /// Navigation property для БД
    /// </summary>
    public TrackedTask Task { get; set; } = null!;

    /// <summary>
    /// Navigation property для БД
    /// </summary>
    public TaskActionType? Type { get; set; }
}
