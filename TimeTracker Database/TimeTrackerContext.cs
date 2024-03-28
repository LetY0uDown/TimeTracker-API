using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeTracker.Database.Models;

namespace TimeTracker.Database;

public sealed class TimeTrackerContext : DbContext
{
    private readonly IConfiguration _config;

    public TimeTrackerContext (IConfiguration config)
    {
        _config = config;
        Database.EnsureCreated();
    }

    public DbSet<Interval> Intervals { get; set; }

    public DbSet<TrackedTask> Tasks { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_config["ConnectionStrings:Default"]);

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackedTask>(entity => {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("ID");

            entity.HasData([
                new TrackedTask {
                    Id = 1,
                    Title = "Уборка",
                    Description = "Помыть пол",
                    IsDone = false
                },
                new TrackedTask {
                    Id = 3,
                    Title = "Зарядка",
                    Description = null,
                    PlannedTime = new TimeSpan(0, 10, 0).Ticks,
                    IsDone = false
                },
                new TrackedTask {
                    Id = 4,
                    Title = "Работа",
                    Description = "Поработать",
                    PlannedTime = new TimeSpan(5, 30, 0).Ticks,
                    IsDone = true,
                    StartedAt = new DateTime(2024, 3, 28, 10, 0, 0, DateTimeKind.Utc)
                },
                new TrackedTask {
                    Id = 5,
                    Title = "Дз",
                    Description = "Сделать дз",
                    PlannedTime = new TimeSpan(1, 0, 0).Ticks,
                    IsDone = true,
                    StartedAt = new DateTime(2000, 10, 8, 15, 45, 0, DateTimeKind.Utc)
                },
                new TrackedTask {
                    Id = 2,
                    Title = "Задачка",
                    Description = "ОПИСАНИЕ КАКОЙ-ТО ЗАДАЧКИ, НО УЖЕ ПОДЛИННЕЕ И КАПСОМ",
                    IsDone = false
                },
            ]);
        });

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

            entity.HasData([
                new Interval {
                    Id = 1,
                    TaskId = 4,
                    CreatedAt = new DateTime(2024, 3, 28, 12, 30, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(2, 30, 0).Ticks
                },

                new Interval {
                    Id = 2,
                    TaskId = 4,
                    CreatedAt = new DateTime(2024, 3, 28, 14, 30, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(4, 30, 0).Ticks
                },

                new Interval {
                    Id = 3,
                    TaskId = 5,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 0, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(0, 15, 0).Ticks
                },

                new Interval {
                    Id = 4,
                    TaskId = 5,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 20, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(0, 35, 0).Ticks
                },

                new Interval {
                    Id = 5,
                    TaskId = 5,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 30, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(0, 45, 0).Ticks
                },

                new Interval {
                    Id = 6,
                    TaskId = 5,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 35, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(0, 50, 0).Ticks
                },

                new Interval {
                    Id = 7,
                    TaskId = 5,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 40, 0, DateTimeKind.Utc),
                    WorkingTime = new TimeSpan(0, 55, 0).Ticks
                }
            ]);
        });
    }
}
