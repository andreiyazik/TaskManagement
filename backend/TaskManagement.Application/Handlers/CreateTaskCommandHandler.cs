using MediatR;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TaskManagement.Application.Commands;
using TaskManagement.Application.Integrations;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Events;
using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Handlers;

public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskAggregate>
{
    private readonly ITaskRepository _repository;
    private readonly IServiceBusHandler _serviceBusHandler;

    public CreateTaskCommandHandler(
        ITaskRepository repository,
        IServiceBusHandler serviceBusHandler)
    {
        _repository = repository;
        _serviceBusHandler = serviceBusHandler;
    }

    public async Task<TaskAggregate> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var result = TaskAggregate.Create(request.Info.Name, request.Info.Description, request.Info.AssignedTo);
        var createdTask = result.Value;

        await _repository.CreateAsync(createdTask, cancellationToken);

        var message = new ServiceBusMessage
        {
            Name = "TaskCreated",
            Payload = JObject.FromObject(createdTask)
        };

        await _serviceBusHandler.PublishMessageAsync(message);

        return createdTask;
    }
}
