using Application.DTO;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.TasksQueries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRepTasksQueryable().Select(x=>x.AsDto()).ToListAsync();
    }
}