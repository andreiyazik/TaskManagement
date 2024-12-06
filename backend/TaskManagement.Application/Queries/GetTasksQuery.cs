using MediatR;
using TaskManagement.Models.Dtos.Tasks;

namespace TaskManagement.Application.Queries;

public sealed record GetTasksQuery(int Skip, int Take = 50) : IRequest<IReadOnlyCollection<TaskDto>>;
