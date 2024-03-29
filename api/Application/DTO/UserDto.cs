using Domain.Entities;

namespace Application.DTO;

public record UserDto(string Email, string FirstName, string LastName, string Role);