using Application.DTO;
using MediatR;

namespace Application.Query.RepoQueries.GetRepoById;

public class GetRepoByIdQuery : IRequest<RepositoryDto>
{
    public Guid Id { get; set; }
}