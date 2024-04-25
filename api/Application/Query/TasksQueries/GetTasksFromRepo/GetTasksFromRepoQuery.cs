using Application.DTO;
using MediatR;

namespace Application.Query.TasksQueries.GetTasksFromRepo;

public class GetTasksFromRepoQuery : IRequest<IEnumerable<TaskDto>>
{
    public Guid RepositoryId { get; set; }
}