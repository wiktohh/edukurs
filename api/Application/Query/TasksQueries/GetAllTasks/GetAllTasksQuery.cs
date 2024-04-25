using Application.DTO;
using MediatR;

namespace Application.Query.TasksQueries.GetAllTasks;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskDto>>
{
    
}