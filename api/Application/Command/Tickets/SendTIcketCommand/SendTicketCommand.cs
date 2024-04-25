using MediatR;

namespace Application.Command.Tickets.SendTIcketCommand;

public class SendTicketCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RepositoryId { get; set; }
}