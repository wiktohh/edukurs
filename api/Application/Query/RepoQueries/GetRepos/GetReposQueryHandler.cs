using Application.DTO;
using Domain.Repositories;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.RepoQueries.GetRepos;

public class GetReposQueryHandler : IRequestHandler<GetReposQuery,ICollection<RepositoryDto>>
{
    private readonly IRepRepository _repository;

    public GetReposQueryHandler(IRepRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ICollection<RepositoryDto>> Handle(GetReposQuery request, CancellationToken cancellationToken)
    {
        var repos = _repository.GetAllRepositoriesAsync();
        return request.RepoEnum switch
        {
            RepoEnum.GetAllRepos => await repos.Select(x => x.AsDto()).ToListAsync(cancellationToken),
            RepoEnum.GetUsersRepos => await repos.Where(x => x.Users.Any(x => x.UserId == new UserId(request.Id)))
                .Select(x => x.AsDto()).ToListAsync(cancellationToken),
            RepoEnum.GetOtherRepos => await repos.Where(x => x.Users.All(x => x.UserId != new UserId(request.Id)))
                .Select(x => x.AsDto()).ToListAsync(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}