using Application.DTO;
using MediatR;

namespace Application.Query.TicketQueries.GetPendingTickets;

public class GetPendingTicketsQuery : IRequest<ICollection<TicketDto>>
{
    public Guid UserId { get; set; }
}