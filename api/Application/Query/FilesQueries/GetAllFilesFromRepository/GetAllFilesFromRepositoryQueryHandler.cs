using Application.DTO;
using Application.Exceptions;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Query.FilesQueries.GetAllFilesFromRepository;

public class GetAllFilesFromRepositoryQueryHandler : IRequestHandler<GetAllFilesFromRepositoryQuery, IEnumerable<ReportDto>>
{
    private readonly IRepRepository _repository;
    private readonly ISubmittedTaskRepository _submittedTaskRepository;
    private readonly IAccountRepository _accountRepository;

    public GetAllFilesFromRepositoryQueryHandler(IRepRepository repository,ISubmittedTaskRepository submittedTaskRepository,IAccountRepository accountRepository)
    {
        _repository = repository;
        _submittedTaskRepository = submittedTaskRepository;
        _accountRepository = accountRepository;
    }
    
    public async Task<IEnumerable<ReportDto>> Handle(GetAllFilesFromRepositoryQuery request, CancellationToken cancellationToken)
    {
        var rep = await _repository.GetRepositoryByIdAsync(request.RepositoryId);
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
            .ThenInclude(x=>x.Repository)
            .Where(x=>x.RepTask.RepositoryId == new RepositoryId(request.RepositoryId))
            .Select(x=>x.AsDto())
            .ToListAsync(cancellationToken: cancellationToken);
        return reports;
    }
}

public class ForbiddenAccessException : BaseException
{
    public ForbiddenAccessException(string message) : base(message)
    {
    }
}