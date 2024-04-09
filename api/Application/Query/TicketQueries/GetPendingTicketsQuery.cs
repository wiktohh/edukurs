using Application.DTO;
using Domain.Entities;
using Domain.ValueObjects.Ticket;
using MediatR;

namespace Application.Query.TicketQueries;

public class GetPendingTicketsQuery : IRequest<ICollection<TicketDto>>
{
    public Guid UserId { get; set; }
}