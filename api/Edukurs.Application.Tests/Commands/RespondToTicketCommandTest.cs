using Application.Command.Tickets.RespondToTicket;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Ticket;
using FluentAssertions;
using Moq;

namespace Edukurs.Application.Tests.Commands;

public class RespondToTicketCommandTest
{
    private readonly Mock<ITicketRepository> _ticketRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IRepRepository> _repRepository;
    private readonly Mock<IAccountRepository> _userRepository;
    
    public RespondToTicketCommandTest()
    {
        _ticketRepository = new();
        _unitOfWork = new();
        _repRepository = new();
        _userRepository = new();
    }

    [Fact]
    public async Task RespondToTicketCommand_ShouldThrowException_WhenTicketNotFound()
    {
        //Arrange
        
        var command = new RespondToTicketCommand()
        {
            TicketId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Status = "Approved"
        };
        
        var handler = new RespondToTicketCommandHandler(_ticketRepository.Object, _unitOfWork.Object, _repRepository.Object, _userRepository.Object);
        
        _ticketRepository
            .Setup(x => x.GetTicketByIdAsync(It.IsAny<TicketId>()))
            .ReturnsAsync((Ticket)null);

        //Act
        var del = async ()=> await handler.Handle(command, default);
        //Assert
        await del.Should().ThrowAsync<NotFoundException>();
    }
}