using FocusFlow.Models;

namespace FocusFlow.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (db.Courses.Any() || db.StudentTasks.Any())
        {
            return;
        }

        var courses = new List<Course>
        {
            new Course { Name = "CSE 340 - Web Backend Development" },
            new Course { Name = "CSE 341 - Web Services" },
            new Course { Name = "PSY 101 - Introduction to Psychology" },
            new Course { Name = "COMM 210 - Public Speaking" },
            new Course { Name = "MATH 108X - College Algebra" }
        };

        db.Courses.AddRange(courses);

        await db.SaveChangesAsync();

        var tasks = new List<StudentTask>
        {
            new StudentTask
            {
                Title = "Finish Blazor CRUD Assignment",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = "High",
                IsCompleted = false
            },

            new StudentTask
            {
                Title = "Study for Psychology Quiz",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = "Medium",
                IsCompleted = false
            },

            new StudentTask
            {
                Title = "Complete Discussion Board Post",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = "Low",
                IsCompleted = true
            },

            new StudentTask
            {
                Title = "Prepare Public Speaking Outline",
                DueDate = DateTime.UtcNow.AddDays(5),
                Priority = "High",
                IsCompleted = false
            },

            new StudentTask
            {
                Title = "Math Homework Chapter 4",
                DueDate = DateTime.UtcNow.AddDays(4),
                Priority = "Medium",
                IsCompleted = false
            },

            new StudentTask
            {
                Title = "Review Notes for Midterm",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "High",
                IsCompleted = false
            }
        };

        db.StudentTasks.AddRange(tasks);

        await db.SaveChangesAsync();
    }
}