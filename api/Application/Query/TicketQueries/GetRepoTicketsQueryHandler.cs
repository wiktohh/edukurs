using Application.DTO;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.TicketQueries;

public class GetRepoTicketsQueryHandler : IRequestHandler<GetRepoTicketsQuery, ICollection<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IRepRepository _repositoryRepository;

    public GetRepoTicketsQueryHandler(ITicketRepository ticketRepository, IRepRepository repositoryRepository)
    {
        _ticketRepository = ticketRepository;
        _repositoryRepository = repositoryRepository;
    }
    
    public async Task<ICollection<TicketDto>> Handle(GetRepoTicketsQuery request, CancellationToken cancellationToken)
    {
        var repo = await _repositoryRepository.GetRepositoryByIdAsync(new RepositoryId(request.RepositoryId));
        if (repo == null)
        {
            throw new Exception("Repository not found");
        }
        if (repo.OwnerId != request.UserId)
        {
            throw new Exception("You are not the owner of this repository");
        }
        var tickets = _ticketRepository.GetAllTicketsAsync().Where(x=>x.RepositoryId== new RepositoryId(request.RepositoryId));
        return  await tickets.Select(x => x.AsDto()).ToListAsync(cancellationToken);
    }
}