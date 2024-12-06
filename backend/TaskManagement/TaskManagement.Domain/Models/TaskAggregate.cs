using TaskManagement.Models.Common;
using TaskManagement.Models.Enums;

namespace TaskManagement.Domain.Models;

public class TaskAggregate
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }
    public string? AssignedTo { get; private set; }

    private TaskAggregate(string name, string description, string? assignedTo)
    {
        Name = name;
        Description = description;
        AssignedTo = assignedTo;
        Status = Status.NotStarted;
    }

    public static Result<TaskAggregate> Create(string name, string description, string? assignedTo)
    {
        var task = new TaskAggregate(name, description, assignedTo);

        return new Result<TaskAggregate>(task);
    }

    public Result ChangeStatus(Status newStatus)
    {
        var errors = IsValidStatusTransition(newStatus);
        if (errors.Any())
        {
            return new Result<TaskAggregate>(errors);
        }

        Status = newStatus;

        return new Result<TaskAggregate>(this);
    }

    private List<Error> IsValidStatusTransition(Status newStatus)
    {
        var errors = new List<Error>();

        var isValidTransition = Status switch
        {
            Status.NotStarted => newStatus == Status.InProgress,
            Status.InProgress => newStatus == Status.NotStarted || newStatus == Status.Completed,
            Status.Completed => false, // No transitions allowed from Completed
            _ => false
        };

        if (!isValidTransition)
        {
            errors.Add(new Error($"Cannot transition from {Status} to {newStatus}."));
        }

        return errors;
    }
}
