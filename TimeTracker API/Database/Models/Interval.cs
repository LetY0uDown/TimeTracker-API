namespace TimeTracker.API.Database.Models;

public class Interval
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public TimeOnly WorkingTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public TrackedTask? Task { get; set; }
}