using MediatR;

namespace Application.Command.Tasks.CreateTask;

public class CreateTaskCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid RepTaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public Guid RepositoryId { get; set; }
}