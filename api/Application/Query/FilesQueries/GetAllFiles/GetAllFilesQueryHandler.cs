using Application.DTO;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.FilesQueries.GetAllFiles;

public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, IEnumerable<ReportDto>>
{
    private readonly ISubmittedTaskRepository _repository;

    public GetAllFilesQueryHandler(ISubmittedTaskRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ReportDto>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var submittedTask = await _repository.GetSubmittedTasksQueryable().Select(x=>x.AsDto()).ToListAsync(cancellationToken: cancellationToken);
        return submittedTask;
    }
}