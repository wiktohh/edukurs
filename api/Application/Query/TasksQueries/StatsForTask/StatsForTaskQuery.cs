using Application.DTO;
using MediatR;

namespace Application.Query.TasksQueries.StatsForTask;

public class StatsForTaskQuery : IRequest<IEnumerable<UserDto>>
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}