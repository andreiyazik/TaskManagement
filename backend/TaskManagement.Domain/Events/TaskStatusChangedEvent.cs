namespace TaskManagement.Domain.Events;

public class TaskStatusChangedEvent
{
    public int TaskId { get; set; }
    public string OldStatus { get; set; }
    public string NewStatus { get; set; }
}
