using Application.Command.UserCommands.SignInUser;
using Application.Command.UserCommands.SignUpUser;
using Application.DTO;
using Application.Exceptions;
using Application.Security;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.User;
using FluentAssertions;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class SignInUserCommandTest
{
    private readonly Mock<IAccountRepository> _accountRepository;
    private readonly Mock<IAuthenticator> _authenticator;
    private readonly Mock<ITokenStorage> _tokenManager;
    private readonly Mock<IPasswordManager> _passwordManager;


    public SignInUserCommandTest()
    {
        _accountRepository = new();
        _passwordManager = new();
        _tokenManager = new();
        _authenticator = new();
    }
    
    
    [Fact]
    public async Task SignInCommand_ShouldThrowException_WhenUserDontExist()
    {
        // Arrange
        var command = new SignInUserCommand()
        {
            Email ="string@gmail.com",
            Password = "password123"
        };
        var handler = new SignInUserCommandHandler(_accountRepository.Object,
            _tokenManager.Object, _authenticator.Object,
            _passwordManager.Object);
        _accountRepository.Setup(x => x.
            isEmailUniqueAsync(It.IsAny<Email>())).ReturnsAsync(false);        
        
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<InvalidCredentialsException>();
    }
    
    [Fact]
    public async Task SignInCommand_ShouldThrowException_WhenPasswordIsInvalid()
    {
        // Arrange
        var command = new SignInUserCommand()
        {
            Email ="string@gmail.com",
            Password = "password123"
        };
        var handler = new SignInUserCommandHandler(_accountRepository.Object,
            _tokenManager.Object, _authenticator.Object,
            _passwordManager.Object);
        
        _accountRepository.Setup(x => x.
            isEmailUniqueAsync(It.IsAny<Email>())).ReturnsAsync(true);
        var user = new User(Guid.NewGuid(),"string2@gmail.com","test", "test","Password","Student");
        _accountRepository.Setup(x => x.GetByEmailAsync(It.IsAny<Email>())).ReturnsAsync(user);
        _passwordManager.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<InvalidCredentialsException>();
    }
    
    
    [Fact]
    public async Task SignInCommand_ShouldReturnToken_WhenEmailAndPasswordAreValid()
    {
        // Arrange
        var command = new SignInUserCommand()
        {
            Email ="string@gmail.com",
            Password = "password123"
        };
        var handler = new SignInUserCommandHandler(_accountRepository.Object,
            _tokenManager.Object, _authenticator.Object,
            _passwordManager.Object);
        
        _accountRepository.Setup(x => x.
            isEmailUniqueAsync(It.IsAny<Email>())).ReturnsAsync(true);
        var user = new User(Guid.NewGuid(),"string2@gmail.com","test", "test","Password","Student");
        _accountRepository.Setup(x => x.GetByEmailAsync(It.IsAny<Email>())).ReturnsAsync(user);
        _passwordManager.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        // Act
        await handler.Handle(command, default);
        // Assert
        _tokenManager.Verify(x=>x.Set(It.IsAny<JwtDto>()),Times.Once);
        _authenticator.Verify(x=>x.CreateToken(user.Id, user.Role),Times.Once);
    }
}