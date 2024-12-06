using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Entities;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementContext _context;
    private readonly IMapper _mapper;

    public TaskRepository(TaskManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskAggregate> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FirstAsync(t => t.Id == id, cancellationToken);

        return _mapper.Map<TaskAggregate>(task);
    }

    public async Task CreateAsync(TaskAggregate task, CancellationToken cancellationToken)
    {
        var taskEntity = _mapper.Map<TaskEntity>(task);

        await _context.Tasks.AddAsync(taskEntity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _mapper.Map(taskEntity, task);
    }

    public async Task UpdateAsync(TaskAggregate task, CancellationToken cancellationToken)
    {
        var taskEntity = await _context.Tasks.FirstAsync(t => t.Id == task.Id, cancellationToken);

        _mapper.Map(task, taskEntity);

        _context.Tasks.Update(taskEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
