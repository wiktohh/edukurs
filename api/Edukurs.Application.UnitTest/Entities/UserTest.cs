using Domain.ValueObjects.User;
using FluentAssertions;

namespace Edukurs.Application.UnitTest.Entities;

public class UserTest
{
    [Theory]
    [InlineData("password")]
    [InlineData("password123")]
    [InlineData("password123!")]
    public void Create_Password_ShouldThrowException_WhenPasswordIsInvalid(string passwordValue)
    {
        // Arrange
        // Act
        Action act = () => new Password(passwordValue);
        
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}