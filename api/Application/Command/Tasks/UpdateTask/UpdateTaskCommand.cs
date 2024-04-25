using MediatR;

namespace Application.Command.Tasks.UpdateTask; 

public class UpdateTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Deadline { get; set; }
}