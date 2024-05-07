using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.FilesQueries.GetAllFilePaths;

public class GetAllFilePathsQueryHandler : IRequestHandler<GetAllFilePathsQuery, string[]>
{
    private readonly ISubmittedTaskRepository _repository;

    public GetAllFilePathsQueryHandler(ISubmittedTaskRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<string[]> Handle(GetAllFilePathsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetSubmittedTasksQueryable().Select(x=>x.Path).ToArrayAsync(cancellationToken: cancellationToken);
    }
}