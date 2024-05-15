using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.RepTask;
using MediatR;

namespace Application.Command.Tasks.CreateTask;

internal class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IRepRepository _repRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(ITaskRepository taskRepository,IRepRepository repRepository,IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _repRepository = repRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var repository = await _repRepository.GetRepositoryByIdAsync(request.RepositoryId);
        if (repository is null)
        {
            throw new NotFoundException("Repository not found");
        }   
        if (repository.OwnerId != request.UserId)
        {
            throw new UnauthorazedException("You are not the owner of this repository");
        }
        RepTaskId repTaskId = new RepTaskId(request.RepTaskId);
        Title title = new Title(request.Title);
        Description description = new Description(request.Description);
        Deadline deadline = new Deadline(request.Deadline);
        RepositoryId repositoryId = new RepositoryId(request.RepositoryId);

        var task = new RepTask
        {
            Id = repTaskId,
            Title = title,
            Description = description,
            Deadline = deadline,
            RepositoryId = repositoryId
        };
        await _taskRepository.AddRepTaskAsync(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}