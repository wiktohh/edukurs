using Application.DTO;
using Application.Exceptions;
using Application.Query.FilesQueries.GetAllFilesFromRepository;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.RepTask;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.FilesQueries.GetAllFilesFromTask;

public class GetAllFilesFromTaskQueryHandler : IRequestHandler<GetAllFilesFromTaskQuery, IEnumerable<ReportDto>>
{
    private readonly IRepRepository _repository;
    private readonly ISubmittedTaskRepository _submittedTaskRepository;
    private readonly IAccountRepository _accountRepository;

    public GetAllFilesFromTaskQueryHandler(IRepRepository repository,ISubmittedTaskRepository submittedTaskRepository,IAccountRepository accountRepository)
    {
        _repository = repository;
        _submittedTaskRepository = submittedTaskRepository;
        _accountRepository = accountRepository;
    }
    
    public async Task<IEnumerable<ReportDto>> Handle(GetAllFilesFromTaskQuery request, CancellationToken cancellationToken)
    {
        var rep = await _repository.GetRepositoryByIdAsync(request.RepTaskId);
        var user = await _accountRepository.GetByIdAsync(request.UserId);
        if (rep == null || user == null)
        {
            throw new NotFoundException("Repository or User not found");
        }

        if (rep.OwnerId != request.UserId)
        {
            throw new ForbiddenAccessException("You are not allowed to access this repository");
        }
        var reports = await _submittedTaskRepository
            .GetSubmittedTasksQueryable()
            .Include(x=>x.RepTask)
            .Where(x=>x.RepTaskId == new RepTaskId(request.RepTaskId))
            .Select(x=>x.AsDto())
            .ToListAsync(cancellationToken: cancellationToken);
        return reports;
    }
}