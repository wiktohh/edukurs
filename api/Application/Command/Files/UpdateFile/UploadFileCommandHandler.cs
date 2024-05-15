using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Command.Files.UpdateFile;

internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
{
    private async Task<string> WriteFile(IFormFile file,User user,RepTask task,Repository repository)
    {
        var pathBuilt = "";
        string filename = "";
        try
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            filename = $"{user.FirstName}_{user.LastName}_{Guid.NewGuid().ToString().Substring(0,7)}{extension}";
            var taskdir = string.Join('_', task.Title.ToString().Split(' '));
            var repdir = string.Join('_', repository.Name.ToString().Split(' '));
            pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",repdir,taskdir);
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",repdir,taskdir,filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return $"{pathBuilt}\\{filename}";
    }
    
    private readonly ISubmittedTaskRepository _repository;
    private readonly ITaskRepository _taskRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRepRepository _repRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadFileCommandHandler(ISubmittedTaskRepository repository,ITaskRepository taskRepository,IAccountRepository accountRepository,IRepRepository repRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _taskRepository = taskRepository;
        _accountRepository = accountRepository;
        _repRepository = repRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        
        var task = await _taskRepository.GetRepTaskAsync(request.TaskId);
        if (task is null)
        {
            throw new NotFoundException("Task not found");
        }

        if (task.Deadline < DateTime.Now)
        {
            throw new BadRequestException("Task is expired");
        }
        var repository = await _repRepository.GetRepositoryByIdAsync(task.RepositoryId);
        if (repository is null)
        {
            throw new NotFoundException("repository not found");
        }
        var user = await _accountRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        if (repository.Users.All(x => x.UserId != user.Id))
        {
            throw new BadRequestException("You are not a member of this repository");
        }
        var path = await WriteFile(request.File,user,task,repository);
        var submittedTask = new SubmittedTask()
        {
            Id = Guid.NewGuid(),
            RepTaskId = task.Id,
            UserId = user.Id,
            Path = path
        };
        await _repository.AddSubmittedTaskAsync(submittedTask);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

