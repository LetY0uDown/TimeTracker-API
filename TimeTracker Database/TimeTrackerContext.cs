using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeTracker.Database.Models;

namespace TimeTracker.Database;

public sealed partial class TimeTrackerContext : DbContext
{
    private readonly IConfiguration _config;

    public TimeTrackerContext (IConfiguration config)
    {
        _config = config;
        Database.EnsureCreated();
    }

    public DbSet<TimestampType> TimestampTypes { get; set; }

    public DbSet<TrackedTask> Tasks { get; set; }

    public DbSet<Timestamp> Timestamps { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_config["ConnectionStrings:Default"]);

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimestampType>(entity => {
            entity.HasKey(e => e.Id).HasName("TimestampType_pkey");

            entity.ToTable("TimestampType");

            entity.Property(e => e.Id)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");
        });

        modelBuilder.Entity<TrackedTask>(entity => {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("ID");
        });

        modelBuilder.Entity<Timestamp>(entity => {
            entity.HasKey(e => e.Id).HasName("Timestamp_pkey");

            entity.ToTable("Timestamp");

            entity.Property(e => e.Id)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Task).WithMany(p => p.Timestamps)
                  .HasForeignKey(d => d.TaskId)
                  .HasConstraintName("TaskID");
            
            entity.HasOne(d => d.Type).WithMany(p => p.Timestamps)
                  .HasForeignKey(d => d.TypeId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("TypeID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
}
