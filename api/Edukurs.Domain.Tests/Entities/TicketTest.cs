using Domain.Exceptions;
using Domain.ValueObjects.Ticket;
using FluentAssertions;

namespace Edukurs.Domain.Tests.Entities;

public class TicketTest
{
    [Theory]
    [InlineData("Approved")]
    [InlineData("Pending")]
    [InlineData("Rejected")]
    public void Create_Status_ShouldSuccess(string statusValue)
    {
        //arrange
        Status status;
        //act   
        status = new Status(statusValue);
        //Assert
        status.Value.Should().Be(statusValue);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("            ")]
    [InlineData("Approved1")]
    [InlineData("Pending1")]
    [InlineData("Rejected1")]
    public void Create_Status_ShouldThrowException_WhenStatusIsInvalid(string status)
    {
        // Arrange
        // Act
        var del = () => new Status(status);
        // Assert
        del.Should().Throw<InvalidStatusException>();
    }
}