using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Tasks.Views;

public sealed record TaskView(int Id, string Name, string Description, Status Status, string? AssignedTo);
