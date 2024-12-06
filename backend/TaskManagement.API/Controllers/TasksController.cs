using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands;
using TaskManagement.Application.Queries;
using TaskManagement.Models.Tasks.Infos;
using TaskManagement.Models.Tasks.Views;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TasksController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    // Create a new task
    [HttpPost]
    public async Task<ActionResult<TaskView>> CreateTask([FromBody] TaskInfo info)
    {
        var createdTask = await _mediator.Send(new CreateTaskCommand(info));
        var result = _mapper.Map<TaskView>(createdTask);

        return Created(Url.ToString(), result);
    }

    // Update task status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] ChangeTaskStatusInfo info)
    {
        await _mediator.Send(new ChangeTaskStatusCommand(id, info));

        return NoContent();
    }

    // Show the list of tasks and their status
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TaskView>>> GetTasks(int skip, int take)
    {
        var tasks = await _mediator.Send(new GetTasksQuery(skip, take));
        var result = _mapper.Map<IReadOnlyCollection<TaskView>>(tasks);

        return Ok(result);
    }
}
