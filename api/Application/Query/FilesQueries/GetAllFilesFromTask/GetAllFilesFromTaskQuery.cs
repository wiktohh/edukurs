using Application.DTO;
using MediatR;

namespace Application.Query.FilesQueries.GetAllFilesFromTask;

public class GetAllFilesFromTaskQuery : IRequest<IEnumerable<ReportDto>>
{
    public Guid UserId { get; set; }
    public Guid RepTaskId { get; set; }
}