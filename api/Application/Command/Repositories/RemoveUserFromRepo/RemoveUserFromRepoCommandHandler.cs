using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Command.Repositories.RemoveUserFromRepo;

internal class RemoveUserFromRepoCommandHandler : IRequestHandler<RemoveUserFromRepoCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRepRepository _repRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserFromRepoCommandHandler(IAccountRepository accountRepository, IRepRepository repRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _repRepository = repRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(RemoveUserFromRepoCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetByIdAsync(request.UserId);   
        var repository = await _repRepository.GetRepositoryByIdAsync(request.RepositoryId);
        var userRepo = repository.Users.FirstOrDefault(x => x.UserId == user.Id && x.RepositoryId == repository.Id);
        if (user == null)
        {
            throw new NotFoundException("User not found in repository");
        }
        if (repository == null)
        {
            throw new NotFoundException("Repository not found in repository");
        }

        if (repository.OwnerId != request.SenderId)
        {
            throw new UnauthorazedException("You are not an owner");
        }
        if (userRepo == null)
        {
            throw new NotFoundException("User not found in repository");
        }
        repository.Users.Remove(userRepo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}