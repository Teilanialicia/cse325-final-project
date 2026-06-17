using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Models;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Course> Courses => Set<Course>();
  public DbSet<StudentTask> StudentTasks => Set<StudentTask>();
  public DbSet<AppUser> Users => Set<AppUser>();
}