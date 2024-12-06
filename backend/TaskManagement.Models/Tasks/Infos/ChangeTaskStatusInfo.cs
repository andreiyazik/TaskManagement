using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Tasks.Infos;

public sealed record ChangeTaskStatusInfo(Status NewStatus);
