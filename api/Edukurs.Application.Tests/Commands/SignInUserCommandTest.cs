using Application.Security;
using Domain.Repositories;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class SignInpUserCommandTest
{
    private readonly Mock<IAccountRepository> _accountRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPasswordManager> _passwordManager;


    public SignUpUserCommandTest()
    {
        _accountRepository = new();
        _unitOfWork = new();
        _passwordManager = new();
    }
}