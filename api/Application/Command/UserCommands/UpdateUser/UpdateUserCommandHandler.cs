using Application.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Command.UserCommands.UpdateUser;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepRepository _repRepository;

    public UpdateUserCommandHandler(IAccountRepository repository, IUnitOfWork unitOfWork,IRepRepository repRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _repRepository = repRepository;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var role = new Role(request.Role);
        if (user.Role == "Teacher" && role.Value == "Student")
        {
            throw new Exception("You can't update teacher role to student role");
        }
        user.UpdateRole(role);
        _repository.UpdateAsync(user);
        var userRep = await _repRepository.GetAllUsersRepositoriesAsync().Where(x => x.UserId == user.Id).ToListAsync();
        foreach (var userRepository in userRep)
        {
            _repository.RemoveUserFromRepository(userRepository);
        }
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (!result)
        {
            throw new SavingChangesException("user");
        }
    }
}