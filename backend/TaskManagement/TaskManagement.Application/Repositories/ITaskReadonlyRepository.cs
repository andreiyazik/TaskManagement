using TaskManagement.Models.Dtos.Tasks;

namespace TaskManagement.Application.Repositories;

public interface ITaskReadonlyRepository
{
    Task<IReadOnlyCollection<TaskDto>> GetAllAsync(int skip, int take, CancellationToken cancellationToken);
}
