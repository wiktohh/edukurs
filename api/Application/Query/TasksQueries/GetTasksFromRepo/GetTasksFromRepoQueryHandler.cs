using Application.DTO;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.TasksQueries.GetTasksFromRepo;

public class GetTasksFromRepoQueryHandler : IRequestHandler<GetTasksFromRepoQuery, IEnumerable<TaskDto>>
{
    private readonly ITaskRepository _repository;

    public GetTasksFromRepoQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TaskDto>> Handle(GetTasksFromRepoQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRepTasksQueryable()
            .Where(x => x.RepositoryId == new RepositoryId(request.RepositoryId))
            .Select(x => x.AsDto())
            .ToListAsync(cancellationToken);
    }
}