using Domain.Entities;

namespace Application.DTO;

public record UserDto(Guid Id,string Email, string FirstName, string LastName, string Role);