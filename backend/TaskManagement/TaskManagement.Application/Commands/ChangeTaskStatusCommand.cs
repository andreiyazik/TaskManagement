using MediatR;
using TaskManagement.Models.Tasks.Infos;

namespace TaskManagement.Application.Commands;

public sealed record ChangeTaskStatusCommand(int TaskId, ChangeTaskStatusInfo Info) : IRequest;
