using Microsoft.EntityFrameworkCore;

namespace FocusFlowStarter.Models;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Course> Courses => Set<Course>();
  public DbSet<StudentTask> StudentTasks => Set<StudentTask>();
}