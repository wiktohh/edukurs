using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Command.Repositories.CreateRepository;

internal class CreateRepositoryCommandHandler : IRequestHandler<CreateRepositoryCommand>
{
    private readonly IRepRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;

    public CreateRepositoryCommandHandler(IRepRepository repository,IUnitOfWork unitOfWork, IAccountRepository accountRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }
    
    public async Task Handle(CreateRepositoryCommand request, CancellationToken cancellationToken)
    {
        var repo = _repository.GetAllRepositoriesAsync().FirstOrDefault(x=>x.Name== request.Name);
        if (repo is not null)
        {
            throw new BadRequestException("Repository with this name already exists");
        }
        
        var Repo = new Repository(request.Id,request.Name , request.OwnerId);
        var user = await _accountRepository.GetByIdAsync(request.OwnerId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }        
        Repo.Users.Add(new UserRepository(Repo.Id,user.Id,true));
        await _repository.AddRepositoryAsync(Repo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", Repo.Name.ToString()));
    }
}