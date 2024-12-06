using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Dtos.Tasks;

public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string? AssignedTo { get; set; }
}
