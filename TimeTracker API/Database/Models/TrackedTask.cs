namespace TimeTracker.API.Database.Models;

public class TrackedTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public TimeOnly? PlannedTime { get; set; }

    public bool IsDone { get; set; }

    public List<Interval>? Intervals { get; set; }
}