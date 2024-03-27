using Microsoft.EntityFrameworkCore;
using TimeTracker.API.Database.Models;

namespace TimeTracker.API.Database;

public sealed class TimeTrackerContext : DbContext
{
    private readonly IConfiguration _config;

    public TimeTrackerContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<Interval> Intervals { get; set; }

    public DbSet<TrackedTask> Tasks { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_config["ConnectionStrings:Default"]);

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Interval>(entity => {
            entity.HasKey(e => e.Id).HasName("Interval_pkey");

            entity.ToTable("Interval");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Task).WithMany(p => p.Intervals)
                  .HasForeignKey(d => d.TaskId)
                  .HasConstraintName("TaskID");
        });

        modelBuilder.Entity<TrackedTask>(entity => {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
        });
    }
}
