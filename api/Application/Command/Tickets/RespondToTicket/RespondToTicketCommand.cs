using MediatR;

namespace Application.Command.Tickets.RespondToTicket;

public class RespondToTicketCommand : IRequest
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; }   
}