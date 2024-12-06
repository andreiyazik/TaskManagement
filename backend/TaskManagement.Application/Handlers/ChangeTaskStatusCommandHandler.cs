using MediatR;
using Newtonsoft.Json.Linq;
using TaskManagement.Application.Commands;
using TaskManagement.Application.Integrations;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Events;

namespace TaskManagement.Application.Handlers;

public sealed class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand>
{
    private readonly ITaskRepository _repository;
    private readonly IServiceBusHandler _serviceBusHandler;

    public ChangeTaskStatusCommandHandler(
        ITaskRepository repository,
        IServiceBusHandler serviceBusHandler)
    {
        _repository = repository;
        _serviceBusHandler = serviceBusHandler;
    }

    public async Task Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.TaskId, cancellationToken);
        if (task == null)
        {
            throw new Exception("Task not found.");
        }

        var oldStatus = task.Status;

        task.ChangeStatus(request.Info.NewStatus);

        await _repository.UpdateAsync(task, cancellationToken);

        var message = new ServiceBusMessage
        {
            Name = "TaskStatusChanged",
            Payload = JObject.FromObject(new
            {
                TaskId = task.Id,
                OldStatus = oldStatus,
                NewStatus = task.Status
            })
        };

        await _serviceBusHandler.PublishMessageAsync(message);
    }
}
