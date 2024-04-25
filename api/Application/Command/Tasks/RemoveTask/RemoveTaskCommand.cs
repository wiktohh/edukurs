using MediatR;

namespace Application.Command.Tasks.RemoveTask;

public class RemoveTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}