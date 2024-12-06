using MediatR;
using TaskManagement.Domain.Models;
using TaskManagement.Models.Tasks.Infos;

namespace TaskManagement.Application.Commands;

public sealed record CreateTaskCommand(TaskInfo Info) : IRequest<TaskAggregate>;
