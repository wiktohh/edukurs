using Application.DTO.Response;
using MediatR;

namespace Application.Query.FilesQueries.DownloadSingleReport;

public class DownloadSingleReportQuery : IRequest<FileDto>
{
    public Guid UserId { get; set; }
    public Guid ReportId { get; set;}
}