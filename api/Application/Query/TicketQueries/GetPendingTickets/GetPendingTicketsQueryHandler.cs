using Application.DTO;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.TicketQueries.GetPendingTickets;

public class GetPendingTicketsQueryHandler : IRequestHandler<GetPendingTicketsQuery,ICollection<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;

    public GetPendingTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<ICollection<TicketDto>> Handle(GetPendingTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = _ticketRepository.GetAllTicketsAsync().Include(x=>x.Repository);
        var pendingTickets = await tickets.Where(x => x.Status == "Pending" && x.Repository.OwnerId == request.UserId).ToListAsync(cancellationToken);
        return pendingTickets.Select(x => x.AsDto()).ToList();
    }
}