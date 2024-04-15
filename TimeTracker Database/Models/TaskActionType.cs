namespace TimeTracker.Database.Models;

public class TaskActionType
{
    public Kind Id { get; set; }

    public string Title { get; set; } = null!;

    /// <summary>
    /// Navigation property для БД
    /// </summary>
    public ICollection<TaskAction> Actions { get; set; } = new List<TaskAction>();

    public enum Kind
    {
        Start, Pause, Resume, Finish
    }
}