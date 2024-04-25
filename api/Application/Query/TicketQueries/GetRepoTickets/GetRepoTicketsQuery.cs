using Application.DTO;
using MediatR;

namespace Application.Query.TicketQueries.GetRepoTickets;

public class GetRepoTicketsQuery : IRequest<ICollection<TicketDto>>
{
   public Guid RepositoryId { get; set; }
   public Guid UserId { get; set; }
}