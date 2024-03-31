using Application.DTO;
using MediatR;

namespace Application.Query.RepoQueries;

public class GetReposQuery : IRequest<ICollection<RepositoryDto>>
{
    public Guid Id { get; set; }
    public RepoEnum RepoEnum { get; set; }= RepoEnum.GetAllRepos;
}