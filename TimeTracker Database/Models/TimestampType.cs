namespace TimeTracker.Database.Models;

public partial class TimestampType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public ICollection<Timestamp> Timestamps { get; set; } = new List<Timestamp>();
}