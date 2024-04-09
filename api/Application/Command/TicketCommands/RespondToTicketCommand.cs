using MediatR;

namespace Application.Command.TicketCommands;

public class RespondToTicketCommand : IRequest
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; }   
}