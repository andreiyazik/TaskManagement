using AutoMapper;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Entities;
using TaskManagement.Models.Dtos.Tasks;
using TaskManagement.Models.Tasks.Views;

namespace TaskManagement.Infrastructure.Mapping;

public class TasksProfile : Profile
{
    public TasksProfile()
    {
        CreateMap<TaskAggregate, TaskView>();
        CreateMap<TaskDto, TaskView>();
        CreateMap<TaskAggregate, TaskEntity>();
        CreateMap<TaskEntity, TaskAggregate>();
    }
}
