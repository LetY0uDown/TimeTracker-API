namespace TimeTracker.Database.Models;

public class TrackedTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public long? PlannedTime { get; set; }

    public bool IsDone { get; set; }

    public bool IsPaused { get; set; }

    public DateTime? StartedAt { get; set; }

    public List<Interval>? Intervals { get; set; } = null;
}