using MediatR;
using TaskManagement.Application.Queries;
using TaskManagement.Application.Repositories;
using TaskManagement.Models.Dtos.Tasks;

namespace TaskManagement.Application.Handlers;

public sealed class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IReadOnlyCollection<TaskDto>>
{
    private readonly ITaskReadonlyRepository _repository;

    public GetTasksQueryHandler(ITaskReadonlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllAsync(request.Skip, request.Take, cancellationToken);

        return tasks;
    }
}
