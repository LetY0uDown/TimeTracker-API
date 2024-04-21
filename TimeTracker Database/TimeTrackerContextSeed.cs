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

        modelBuilder.Entity<TaskActionType>(entity => {
            entity.HasData([
                new TaskActionType {
                    Id = TaskActionType.Kind.Start,
                    Title = "Начало"
                },
                new TaskActionType {
                    Id = TaskActionType.Kind.Pause,
                    Title = "Пауза"
                },
                new TaskActionType {
                    Id = TaskActionType.Kind.Resume,
                    Title = "Продолжение"
                },
                new TaskActionType {
                    Id = TaskActionType.Kind.Finish,
                    Title = "Завершение"
                },
            ]);
        });

        modelBuilder.Entity<TaskAction>(entity => {
            entity.HasData([
                new TaskAction {
                    Id = 1,
                    TaskId = 4,
                    TypeId = TaskActionType.Kind.Start,
                    CreatedAt = new DateTime(2024, 3, 28, 12, 30, 0).Ticks
                },

                new TaskAction {
                    Id = 2,
                    TaskId = 4,
                    TypeId = TaskActionType.Kind.Finish,
                    CreatedAt = new DateTime(2024, 3, 28, 14, 30, 0).Ticks
                },

                new TaskAction {
                    Id = 3,
                    TaskId = 5,
                    TypeId = TaskActionType.Kind.Start,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 0, 0).Ticks
                },

                new TaskAction {
                    Id = 4,
                    TaskId = 5,
                    TypeId = TaskActionType.Kind.Pause,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 20, 0).Ticks
                },

                new TaskAction {
                    Id = 5,
                    TaskId = 5,
                    TypeId = TaskActionType.Kind.Resume,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 30, 0).Ticks
                },

                new TaskAction {
                    Id = 6,
                    TaskId = 5,
                    TypeId = TaskActionType.Kind.Finish,
                    CreatedAt = new DateTime(2000, 10, 8, 16, 35, 0).Ticks
                },

                new TaskAction {
                    Id = 7,
                    TaskId = 1,
                    TypeId = TaskActionType.Kind.Start,
                    CreatedAt = new DateTime(2019, 5, 11, 21, 20, 0).Ticks
                }
            ]);
        });
    }
}
