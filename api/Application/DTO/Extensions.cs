using Domain.Entities;

namespace Application.DTO;

public static class Extensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto(user.FirstName, user.LastName, user.Email, user.Role);
    }
}