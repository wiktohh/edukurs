using System.Linq.Expressions;
using Application.Command.UserCommands.UpdateUser;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.User;
using FluentAssertions;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class UpdateUserCommandTest
{
    
    private readonly Mock<IAccountRepository> _repository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IRepRepository> _repRepository;
    
    public UpdateUserCommandTest()
    {
        _repository = new();
        _unitOfWork = new();
        _repRepository = new();
    }

    [Fact]
    public async Task UpdateUserCommand_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            Id = Guid.NewGuid(),
            Role = "Admin"
        };
        var handler = new UpdateUserCommandHandler(_repository.Object, _unitOfWork.Object, _repRepository.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync((User)null);
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task UpdateUserCommand_ShouldThrowException_WhenSavingChangesFailed()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            Id = Guid.NewGuid(),
            Role = "Admin"
        };
        var handler = new UpdateUserCommandHandler(_repository.Object, _unitOfWork.Object, _repRepository.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(Helper.GetUser());
        _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(false);
        
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<SavingChangesException>();
    }
    
    [Fact]
    public async Task UpdateUserCommand_ShouldThrowException_WhenRoleIsInvalid()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            Id = Guid.NewGuid(),
            Role = "Student"
        };
        var handler = new UpdateUserCommandHandler(_repository.Object, _unitOfWork.Object, _repRepository.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(Helper.GetUser());
        
        // Act
        var del = async()=>await handler.Handle(command, default);
        
        // Assert
        await del.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateUserCommand_ShouldSucced_WhenValid()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            Id = Guid.NewGuid(),
            Role = "Admin"
        };
        var handler = new UpdateUserCommandHandler(_repository.Object, _unitOfWork.Object, _repRepository.Object);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(Helper.GetUser());
        _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(true);
        // Act
        await handler.Handle(command, default);
        
        // Assert
        _repository.Verify(x=>x.UpdateAsync(It.IsAny<User>()),Times.Once);
        _repRepository.Verify(x=>x.GetAllUsersRepositoriesAsync(),Times.Once);

        _unitOfWork.Verify(x=>x.SaveChangesAsync(default),Times.Once);
    }

}