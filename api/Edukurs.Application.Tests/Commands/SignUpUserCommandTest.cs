using Application.Command.UserCommands.SignUpUser;

namespace Edukurs.Domain.Tests.Commands;

public class SignUpUserCommandTest
{
    [Fact]
    public async Task CreateUserCommand_ShouldFailResult_WhenUserAlreadyExists()
    {
        // Arrange

        var command = new SignUpUserCommand();
        var handler = new SignUpUserCommandHandler();
        
        // Act
        
        // Assert
}