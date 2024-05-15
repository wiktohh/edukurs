using Application.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using MediatR;

namespace Application.Command.Repositories.UpdateRepository;

internal class UpdateRepositoryCommandHandler : IRequestHandler<UpdateRepositoryCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRepRepository _repRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRepositoryCommandHandler(IAccountRepository accountRepository, IRepRepository repRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _repRepository = repRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateRepositoryCommand request, CancellationToken cancellationToken)
    {
        var repository = await _repRepository.GetRepositoryByIdAsync(request.Id);
        if (repository is null)
        {
            throw new NotFoundException("Repository not found");
        }

        if (repository.OwnerId != request.SenderId)
        {
            throw new UnauthorazedException("You are not the owner of this repository");
        }
        
        repository.Name = new Name(request.Name);
        _repRepository.UpdateRepository(repository);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}