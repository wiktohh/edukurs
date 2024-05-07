using Application.DTO;
using MediatR;

namespace Application.Query.FilesQueries.GetAllFiles;

public class GetAllFilesQuery : IRequest<IEnumerable<ReportDto>>
{
    
}