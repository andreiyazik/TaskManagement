using TaskManagement.Models.Enums;

namespace TaskManagement.Infrastructure.Entities;

public class TaskEntity : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string? AssignedTo { get; set; }
}
