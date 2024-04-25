using Application.DTO;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Query.TasksQueries.GetTaskById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var repTask = await _repository.GetRepTaskAsync(request.TaskId);
        if (repTask == null)
        {
            throw new NotFoundException("Task not found");
        }
        return repTask.AsDto();
    }
}