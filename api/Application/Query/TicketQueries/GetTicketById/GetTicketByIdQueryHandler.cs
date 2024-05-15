using Application.DTO;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Query.TicketQueries.GetTicketById;

internal class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly ITicketRepository _repository;

    public GetTicketByIdQueryHandler(ITicketRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket =  await _repository.GetTicketByIdAsync(request.TicketId);
        return ticket is null ? throw new NotFoundException("Ticket not found") : ticket.AsDto();
    }
}