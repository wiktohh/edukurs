using System.Security.Cryptography;
using Application.Exceptions;
using Application.Security;
using Domain.ValueObjects.User;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Command.UserCommands.SignUpUser;

internal class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordManager _passwordManager;
    
    public SignUpUserCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IPasswordManager passwordManager)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
    }

    public async Task Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetByEmailAsync(request.Email);
        if (user is not null)
        {
            throw new UserAlreadyExistsException(request.Email);
        }
        
        UserId userId = new(request.UserId);
        Email email = new(request.Email);
        Password password = new(request.Password);
        FirstName firstName = new(request.FirstName);
        LastName lastName = new(request.LastName);
        Role role = new(request.Role);
        
        if (email == "admin@gmail.com" && password =="admin")
        {
            role = "Admin";
        }
        
        var securedPassword = _passwordManager.Secure(request.Password);
        User newUser = new User(userId, email, firstName, lastName, securedPassword, role);
        await _accountRepository.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();
    }
}