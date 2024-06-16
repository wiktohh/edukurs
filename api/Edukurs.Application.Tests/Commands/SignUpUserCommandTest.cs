using System.Security.AccessControl;
using Application.Command.UserCommands.SignUpUser;
using Application.Exceptions;
using Application.Security;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.User;
using FluentAssertions;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class SignUpUserCommandTest
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

    [Fact]
    public async Task SignUpCommand_ShouldFailResult_WhenUserAlreadyExists()
    {
        // Arrange

        var command = new SignUpUserCommand()
        {
            Email = "string22222222@gmail.com",
            UserId = Guid.NewGuid(),
            FirstName = "test",
            LastName = "test",
            Password = "password123",
            Role = "Student"
        };
        _accountRepository.Setup(x => x.
            isEmailUniqueAsync(It.IsAny<Email>())).ReturnsAsync(false);        
        var handler = new SignUpUserCommandHandler(_accountRepository.Object
            , _unitOfWork.Object,
            _passwordManager.Object);

        // Act
         var del = async()=>await handler.Handle(command, default);
        // Assert
        await del.Should().ThrowAsync<UserAlreadyExistsException>();
    }

    [Fact]
    public async Task SignUpCommand_ShouldSuccessResult_WhenValidData()
    {
        // Arrange
        var Password = "password";
        var command = new SignUpUserCommand()
        {
            Email = "string2@gmail.com",
            UserId = Guid.NewGuid(),
            FirstName = "test",
            LastName = "test",
            Password = Password,
            Role = "Student"
            
        };
        _accountRepository.Setup(x => x.
            isEmailUniqueAsync(It.IsAny<Email>())).ReturnsAsync(true);        
        var handler = new SignUpUserCommandHandler(_accountRepository.Object
            , _unitOfWork.Object,
            _passwordManager.Object);
        _passwordManager.Setup(x=>x.Secure(It.IsAny<string>())).Returns("securedPassword");
        // Act
        await handler.Handle(command, default);
        // Assert
        _passwordManager.Verify(x=>x.Secure(It.Is<string>(x=>x==Password)),Times.Once);
        _accountRepository.Verify(x=>x.AddAsync(It.IsAny<User>()),Times.Once);
        _unitOfWork.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
    }
}   