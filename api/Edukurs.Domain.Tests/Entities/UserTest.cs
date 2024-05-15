using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects.User;
using FluentAssertions;

namespace Edukurs.Domain.Tests.Entities;

public class UserTest
{
    private User user;

    public UserTest()
    {
        var email = new Email("string@gmail.com");
        var password = new Password("Password123!");
        var firstName = new FirstName("FirstName");
        var lastName = new LastName("LastName");
        var role = new Role("Student");
        user = new User(new UserId(Guid.NewGuid()), email, firstName, lastName, password, role);
    }
    
    [Theory]
    [InlineData("Password")]
    [InlineData("Password123")]
    [InlineData("Password123!")]
    [InlineData("Password123!@#")]
    [InlineData("Password123!@#123")]
    public void Create_password_ShouldSuccess(string passwordValue)
    {
        //arrange
        Password password;
        //act   
        password = new Password(passwordValue);
        //Assert
        password.Value.Should().Be(passwordValue);
    }
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("            ")]
    public void Create_password_shouldThrowException_WhenPasswordIsInvalid(string pass)
    {
        // Arrange
        // Act
        var del = () => new Password(pass);
        // Assert
        del.Should().Throw<InvalidPasswordException>();
    }
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("            ")]
    [InlineData("email")]
    [InlineData("email@")]
    [InlineData("email@com")]
    [InlineData("email@com.")]
    [InlineData("email.com")]
    [InlineData("email.")]
    public void Create_Email_ShouldThrowException_WhenEmailIsInvalid(string email)
    {
        // Arrange
        // Act
        var del = () => new Email(email);
        // Assert
        del.Should().Throw<InvalidEmailException>();
    }
    [Theory]
    [InlineData("goodmail@gmail.com")]
    [InlineData("gol@gml.com")]
    [InlineData("gil@gmail.mm")]
    public void Create_Email_ShouldSuccess_WhenEmailIsValid(string email)
    {
        // Arrange
        // Act
        var emailObj = new Email(email);
        // Assert
        emailObj.Value.Should().Be(email);
    }
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("            ")]
    [InlineData("       Marcin2001XD   ")]
    public void Create_FirstName_ShouldThrowException_WhenFirstNameIsInvalid(string firstName)
    {
        // Arrange
        // Act
        var del = () => new FirstName(firstName);
        // Assert
        del.Should().Throw<InvalidFirstNameException>();
    }
    [Theory]
    [InlineData("FirstName")]
    [InlineData("Fir")]
    [InlineData("FirName")]
    public void Create_FirstName_ShouldSuccess_WhenFirstNameIsValid(string firstName)
    {
        // Arrange
        // Act
        var firstNameObj = new FirstName(firstName);
        // Assert
        firstNameObj.Value.Should().Be(firstName);
    }
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("            ")]
    [InlineData("       Marcin2001XD   ")]
    public void Create_LastName_ShouldThrowException_WhenLastNameIsInvalid( string lastName)
    {
        // Arrange
        // Act
        var del = () => new LastName(lastName);
        // Assert
        del.Should().Throw<InvalidLastNameException>();
    }
    [Theory]
    [InlineData("FirstName")]
    [InlineData("Fir")]
    [InlineData("FirName")]
    public void Create_LastName_ShouldSuccess_WhenLastNameIsValid( string lastName)
    {
        // Arrange
        // Act
        var lastNameObj = new LastName(lastName);
        // Assert
        lastNameObj.Value.Should().Be(lastName);
    }
    [Theory]
    [InlineData("FirstName     ", "FirstName")]
    [InlineData("     Fir", "Fir")]
    [InlineData("     FirName     ","FirName")]
    public void Create_LastName_ShouldSuccess_WhenLastNameIsValidButHaveWhiteSpaces( string lastName, string expectedLastName)
    {
        // Arrange
        // Act
        var lastNameObj = new LastName(lastName);
        // Assert
        lastNameObj.Value.Should().Be(expectedLastName);
    }
    [Fact]
    public void UpdateRoleToAdmin_ShouldSuccess_WhenValid()
    {
        // Arrange
        var role = new Role("Admin");
        // Act
        user.UpdateRole(role);
        // Assert
        user.Role.Should().Be(role);
    }
    
    [Fact]
    public void UpdateRoleToTeacher_ShouldSuccess_WhenValid()
    {
        // Arrange
        var role = new Role("Teacher");
        // Act
        user.UpdateRole(role);
        // Assert
        user.Role.Should().Be(role);
    }
    
    [Fact]
    public void UpdateRole_ShouldThrowException_WhenRoleNotAvailable()
    {
        // Arrange
        var del =()=> new Role("Moderator");
        // Act
        // Assert
        del.Should().Throw<InvalidRoleException>();
    }
}