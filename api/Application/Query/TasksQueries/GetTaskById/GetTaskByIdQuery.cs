using Application.DTO;
using MediatR;

namespace Application.Query.TasksQueries.GetTaskById;

public class GetTaskByIdQuery : IRequest<TaskDto>
{
    public Guid TaskId { get; set; }
}