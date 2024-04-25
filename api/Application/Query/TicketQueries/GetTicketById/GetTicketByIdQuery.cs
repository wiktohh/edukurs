using Application.DTO;
using MediatR;

namespace Application.Query.TicketQueries.GetTicketById;

public class GetTicketByIdQuery : IRequest<TicketDto>
{
    public Guid TicketId { get; set; }
}