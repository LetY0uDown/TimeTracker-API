namespace TimeTracker.Database.Models;

public class Timestamp
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public long CreatedAt { get; set; }

    /// <summary>
    /// 1 - Start; 2 - Pause; 3 - Resume; 4 - Finish
    /// </summary>
    public int? TypeId { get; set; }

    public TrackedTask Task { get; set; } = null!;

    public TimestampType? Type { get; set; }
}
