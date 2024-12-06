using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Models.Dtos.Tasks;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskReadonlyRepository : ITaskReadonlyRepository
{
    private readonly TaskManagementContext _context;

    public TaskReadonlyRepository(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<TaskDto>> GetAllAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Skip(skip)
            .Take(take)
            .Select(task => new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                AssignedTo = task.AssignedTo,
            })
            .ToListAsync(cancellationToken);
    }
}
