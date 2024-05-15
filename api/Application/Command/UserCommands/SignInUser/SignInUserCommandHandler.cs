using Application.Exceptions;
using Application.Security;
using Domain.Repositories;
using MediatR;

namespace Application.Command.UserCommands.SignInUser;

internal class SignInUserCommandHandler : IRequestHandler<SignInUserCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenStorage _tokenManager;
    
    public SignInUserCommandHandler(IAccountRepository accountRepository, ITokenStorage tokenManager, IAuthenticator authenticator, IPasswordManager passwordManager)
    {
        _accountRepository = accountRepository;
        _tokenManager = tokenManager;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
    }
    
    public async Task Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
        
        if (!_passwordManager.Validate(request.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        if (_passwordManager.Validate(request.Password, user.Password))
        {
            var jwt = _authenticator.CreateToken(user.Id, user.Role);
            _tokenManager.Set(jwt);
        }
    }
}