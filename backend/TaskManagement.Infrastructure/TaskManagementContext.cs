using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Entities;

namespace TaskManagement.Infrastructure;

public class TaskManagementContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>().HasKey(t => t.Id);
    }
}
