using Application.DTO;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetAllUsers;

public class GetAllUsersQuery : IRequest<ICollection<UserDto>>
{
    
}