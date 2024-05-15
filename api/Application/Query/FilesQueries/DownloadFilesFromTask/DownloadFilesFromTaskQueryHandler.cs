using System.IO.Compression;
using Application.DTO.Response;
using Domain.Repositories;
using Domain.ValueObjects.RepTask;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.FilesQueries.DownloadFilesFromTask;

internal class DownloadFilesFromTaskQueryHandler : IRequestHandler<DownloadFilesFromTaskQuery,FileDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ISubmittedTaskRepository _submittedTaskRepository;
    private readonly IRepRepository _repository;

    public DownloadFilesFromTaskQueryHandler(ITaskRepository taskRepository,
        ISubmittedTaskRepository submittedTaskRepository,
        IRepRepository repository)
    {
        _taskRepository = taskRepository;
        _submittedTaskRepository = submittedTaskRepository;
        _repository = repository;
    }
    
    public async Task<FileDto> Handle(DownloadFilesFromTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _submittedTaskRepository
            .GetSubmittedTasksQueryable().Include(x=>x.RepTask)
            .Where(x => x.RepTaskId == new RepTaskId(request.TaskId)).ToListAsync();
        var path= task.First().Path;
        var correct = path.Substring(0,path.LastIndexOf('\\'));
        var repo = await _repository.GetRepositoryByIdAsync(task.First().RepTask.RepositoryId);
        if (repo.OwnerId != request.UserId)
        {
            throw new UnauthorizedAccessException();
        }        
        var directory = new DirectoryInfo(correct);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException();
        }

        var files = directory.GetFiles("*.*", SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            throw new DirectoryNotFoundException();
        }

        var zipMemoryStream = new MemoryStream();
        using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
        {
            foreach (var file in files)
            {
                var entry = zipArchive.CreateEntry(file.Name, CompressionLevel.Fastest);

                using (var entryStream = entry.Open())
                using (var fileStream = file.OpenRead())
                {
                    await fileStream.CopyToAsync(entryStream);
                }
            }
        }

        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        FileDto fileDto = new FileDto(zipMemoryStream, correct);
        return fileDto;
    }
}