using Application.DTO;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<UserDto>
{
    public string Email { get; set; }
}