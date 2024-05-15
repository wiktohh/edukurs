using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Command.UserCommands.DeleteUser;

internal class DeleteUserCommandQuery : IRequestHandler<DeleteUserCommand>
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandQuery(IAccountRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        _repository.DeleteAsync(user);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (!result)
        {
            throw new SavingChangesException("user");
        }
    }
}