using Application.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects.RepTask;
using MediatR;

namespace Application.Command.Tasks.UpdateTask;

internal class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IRepRepository _repRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository,IRepRepository repRepository,IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _repRepository = repRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        
        var task = await _taskRepository.GetRepTaskAsync(request.Id);
        if (task is null)
        {
            throw new NotFoundException("Task not found");
        }
        var repository = await _repRepository.GetRepositoryByIdAsync(task.RepositoryId);
        if (repository is null)
        {
            throw new NotFoundException("Repository not found");
        }   
        if (repository.OwnerId != request.UserId)
        {
            throw new UnauthorazedException("You are not the owner of this repository");
        }

        task.Deadline = new Deadline(request.Deadline);
        _taskRepository.UpdateRepTask(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}