using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories;

public interface ITaskRepository
{
    Task<TaskAggregate> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(TaskAggregate task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskAggregate task, CancellationToken cancellationToken);
}
