using Application.DTO;
using MediatR;

namespace Application.Query.FilesQueries.GetAllFilesFromRepository;

public class GetAllFilesFromRepositoryQuery : IRequest<IEnumerable<ReportDto>>
{
    public Guid UserId { get; set; }
    public Guid RepositoryId { get; set; }
}