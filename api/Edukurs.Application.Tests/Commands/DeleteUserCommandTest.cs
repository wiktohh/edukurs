using Application.Command.UserCommands.DeleteUser;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.User;
using FluentAssertions;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class DeleteUserCommandTest
{
    private readonly Mock<IAccountRepository> _repository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public DeleteUserCommandTest()
    {
        _repository = new();
        _unitOfWork = new();
    }
    
    [Fact]
    public async Task DeleteUserCommand_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        var command = new DeleteUserCommand()
        {
            Id = Guid.NewGuid()
        };
        var handler = new DeleteUserCommandQuery(_repository.Object, _unitOfWork.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync((User)null);
        
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task DeleteUserCommand_ShouldThrowException_WhenSavingChangesFailed()
    {
        // Arrange
        var command = new DeleteUserCommand()
        {
            Id = Guid.NewGuid()
        };
        var handler = new DeleteUserCommandQuery(_repository.Object, _unitOfWork.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(Helper.GetUser());
        _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(false);
        
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<SavingChangesException>();
    }

    [Fact]
    public async Task DeleteUserCommand_ShouldSucced_WhenValid()
    {
        // Arrange
        var command = new DeleteUserCommand()
        {
            Id = Guid.NewGuid()
        };
        var handler = new DeleteUserCommandQuery(_repository.Object, _unitOfWork.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(Helper.GetUser());
        _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(true);
        
        // Act
        await handler.Handle(command, default);
        
        // Assert
        _repository.Verify(x=>x.DeleteAsync(It.IsAny<User>()),Times.Once);
        _unitOfWork.Verify(x=>x.SaveChangesAsync(default),Times.Once);
    }
}