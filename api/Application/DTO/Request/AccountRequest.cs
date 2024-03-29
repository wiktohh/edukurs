namespace Application.DTO.Request;

public record AccountSignUp(string Email, string FirstName, string LastName, string Password, string Role);
public record AccountLogin(string Email, string Password);