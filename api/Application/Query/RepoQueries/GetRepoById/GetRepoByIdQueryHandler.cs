using Application.DTO;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Query.RepoQueries.GetRepoById;

internal class GetRepoByIdQueryHandler : IRequestHandler<GetRepoByIdQuery, RepositoryDto>
{
    private readonly IRepRepository _repository;

    public GetRepoByIdQueryHandler(IRepRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<RepositoryDto> Handle(GetRepoByIdQuery request, CancellationToken cancellationToken)
    {
        var repo =  await _repository.GetRepositoryByIdAsync(request.Id);
        return repo is null ? throw new NotFoundException("Rep not found") : repo.AsDto();
    }
}