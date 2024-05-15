using Application.DTO;
using Domain.Repositories;
using Domain.ValueObjects.RepTask;
using Domain.ValueObjects.SubmittedTask;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.TasksQueries.StatsForTask;

internal class StatsForTaskQueryHandler : IRequestHandler<StatsForTaskQuery,IEnumerable<UserDto>>
{
    private readonly ISubmittedTaskRepository _reportRepository;
    private readonly IRepRepository _repRepository;
    private readonly ITaskRepository _taskRepository;

    public StatsForTaskQueryHandler(ISubmittedTaskRepository reportRepository, IRepRepository repRepository,ITaskRepository taskRepository)
    {
        _reportRepository = reportRepository;
        _repRepository = repRepository;
        _taskRepository = taskRepository;
    }
    public async Task<IEnumerable<UserDto>> Handle(StatsForTaskQuery request, CancellationToken cancellationToken)
    {
        var reports = await _reportRepository.GetSubmittedTasksQueryable().Where(x => x.RepTaskId == new RepTaskId(request.TaskId)).ToListAsync(cancellationToken: cancellationToken);
        var task = await _taskRepository.GetRepTaskAsync(request.TaskId);
        var reps = await _repRepository.GetRepositoryByIdAsync(task.RepositoryId);
        if (reps.OwnerId != request.UserId)
        {
            throw new UnauthorizedAccessException();
        }
        var users = reps.Users.Where(x => reports.Any(y => y.UserId == x.UserId)).Select(x=> x.User);
        return users.Select(x => x.AsDto());
    }
}