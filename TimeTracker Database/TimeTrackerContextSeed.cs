using Microsoft.EntityFrameworkCore;
using TimeTracker.Database.Models;

namespace TimeTracker.Database;

public sealed partial class TimeTrackerContext : DbContext
{
    private partial void OnModelCreatingPartial (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackedTask>(entity => {
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
                    IsDone = true
                },
                new TrackedTask {
                    Id = 5,
                    Title = "Дз",
                    Description = "Сделать дз",
                    PlannedTime = new TimeSpan(1, 0, 0).Ticks,
                    IsDone = true
                },
                new TrackedTask {
                    Id = 2,
                    Title = "Задачка",
                    Description = "ОПИСАНИЕ КАКОЙ-ТО ЗАДАЧКИ, НО УЖЕ ПОДЛИННЕЕ И КАПСОМ",
                    IsDone = false
                },
            ]);
        });

        modelBuilder.Entity<TimestampType>(entity => {
            entity.HasData([
                new TimestampType {
                    Id = 1,
                    Title = "Начало"
                },
                new TimestampType {
                    Id = 2,
                    Title = "Пауза"
                },
                new TimestampType {
                    Id = 3,
                    Title = "Продолжение"
                },
                new TimestampType {
                    Id = 4,
                    Title = "Завершение"
                },
            ]);
        });

        modelBuilder.Entity<Timestamp>(entity => {
            entity.HasData([
                new Timestamp {
                    Id = 1,
                    TaskId = 4,
                    TypeId = 1,
                    CreatedAt = new DateTime(2024, 3, 28, 12, 30, 0).Ticks
                },

                new Timestamp {
                    Id = 2,
                    TaskId = 4,
                    TypeId = 4,
                    CreatedAt = new DateTime(2024, 3, 28, 14, 30, 0).Ticks
                },

                new Timestamp {
                    Id = 3,
                    TaskId = 5,
                    TypeId = 1,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 0, 0).Ticks
                },

                new Timestamp {
                    Id = 4,
                    TaskId = 5,
                    TypeId = 2,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 20, 0).Ticks
                },

                new Timestamp {
                    Id = 5,
                    TaskId = 5,
                    TypeId = 3,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 30, 0).Ticks
                },

                new Timestamp {
                    Id = 6,
                    TaskId = 5,
                    TypeId = 4,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 35, 0).Ticks
                }
            ]);
        });
    }
}
