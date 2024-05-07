using Application.DTO.Response;
using MediatR;

namespace Application.Query.FilesQueries.DownloadFilesFromTask;

public class DownloadFilesFromTaskQuery : IRequest<FileDto>
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
}