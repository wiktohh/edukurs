using Application.DTO;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetUserById;

public class GetUserByIdQuery : IRequest<UserDto>
{
   public Guid Id { get; set; }
}