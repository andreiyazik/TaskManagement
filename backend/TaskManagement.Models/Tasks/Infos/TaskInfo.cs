using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.Tasks.Infos;

public sealed record TaskInfo([Required]string Name, string Description, string? AssignedTo);
